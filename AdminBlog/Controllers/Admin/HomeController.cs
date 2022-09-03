using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using Microsoft.EntityFrameworkCore;
namespace AdminBlog.Controllers;

using System.Net;
using System.Net.Mail;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;
    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Login(string email,string password){
        if(email=="admin@gmail.com" && password=="admin"){
            HttpContext.Session.SetInt32("id",0);
            
            
            return(RedirectToAction(nameof(Index)));
        }else{
         var author = _context.Authors.FirstOrDefault(a=> a.Email== email && a.Password == password);
            if(author==null){
            return this.BadRequest("Belə yazar tapılmadı");
            }else{
            HttpContext.Session.SetInt32("id",author.Id);
            return this.Ok("Uğurlu oldu");
        }
        }
        
    }
    public IActionResult Logout(){
        HttpContext.Session.Remove("id");
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory(Category category){
        if(category.Id ==0){
        await _context.AddAsync(category);
        }else{
            _context.Update(category);
        }
        
        await _context.SaveChangesAsync();
        var categories = _context.Categories.ToList();
        return Json(categories);

        
    }
  public async Task<IActionResult> CategoryDetails(int? id){
    Category category = await _context.Categories.FindAsync(id);
    return Json(category);
  }
     public async Task<IActionResult> DeleteCategory(int? id){
        
        Category category = await _context.Categories.FindAsync(id);
        _context.Remove(category);
        
        await _context.SaveChangesAsync();
        return this.Ok("Operation is completed succesfully!");
    }
    public async Task<IActionResult> AuthorDetails(int? id){
        Author author = await _context.Authors.FindAsync(id);
        return Json(author);

    }
    public async Task<IActionResult> AddAuthor(Author author){
        if(author.Id==0){
             await _context.AddAsync(author);
             
                        }
        else{
            _context.Update(author);
            }
            await _context.SaveChangesAsync();
                var authors = await _context.Authors.ToListAsync();
                var jsonAuthors =  Json(authors);
                return(jsonAuthors);
        
       
    }
    public async Task<IActionResult> DeleteAuthor(int? id){
        Author author = await _context.Authors.FindAsync(id);
       _context.Remove(author);
       await _context.SaveChangesAsync();
        return this.Ok("Author is deleted");

    }
    [Route("AdminPanel")]
    public IActionResult Index()
    {
        
        return View();
    }
    
    public IActionResult Category()
    {   
        
        List<Category> categories =  _context.Categories.ToList();
        return View(categories);
    }
    public IActionResult Author(){
        
        return View(_context.Authors.ToList());
    }
    public IActionResult UserMessages(int id){
        ViewBag.context = _context;
        ViewBag.data=_context.UserMessages.ToList();
        if(id!=0){
            var userMessage = _context.UserMessages.FirstOrDefault(um=>um.Id==id);
            userMessage.IsReaded = true;
            _context.Update(userMessage);
            _context.SaveChanges();
            ViewBag.data=userMessage;
        }
        
        
        return View();
        
        
    }
    public IActionResult SendEmail(string receiver,string subject, string message){
      
                if(ModelState.IsValid){
                    var senderEmail = new MailAddress("hssjxie@gmail.com","Məhəmməd Sadıqzadə");
                    var receiverEmail = new MailAddress(receiver,"Receiver");
                    var password = "**********"; //Here is a generated password from google 
                    var sub = subject;
                    var content = message;
                    var smtp = new SmtpClient{
                        Host ="smtp.gmail.com",
                        Port= 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address,password)
                    };
                    using(var mess = new MailMessage(senderEmail,receiverEmail){
                        Subject = sub,
                        Body = content
                    }){
                        smtp.Send(mess);
                        
                    }
                }
            return RedirectToAction("UserMessages");
       
                } 

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
