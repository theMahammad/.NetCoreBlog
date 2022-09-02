using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;

namespace AdminBlog.Controllers;

public class UserHomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;

    public UserHomeController(ILogger<HomeController> logger,BlogContext context)
    {
        _logger = logger;
        _context=context;
    }
    [Route("SendMessage")]
    public async Task<IActionResult> SendMessage(UserMessage message){
        
       if(message!=null)
        {
        await _context.AddAsync(message);
        await _context.SaveChangesAsync();
        return this.Ok("Mesaj göndərildi");
       
        
       }else{
            return this.BadRequest("Mesaj göndərilmədi");
       }
        
    }

    public IActionResult Index()
    {
        var blogs=_context.Blogs.ToList();
        ViewBag.context= _context;
        
        return View(blogs);
    
    }
    [Route("Post")]
      public IActionResult Post(string T){
        var selectedBlog = _context.Blogs.FirstOrDefault(b=> b.Slug==T);
        ViewBag.context = _context;
        return View(selectedBlog);
    }
    [Route("AllPosts")]
    public IActionResult AllPosts(){
        var blogs = _context.Blogs.ToList();
        ViewBag.context = _context;
        return View(blogs);
    }
    [Route("Contact")]
    public ActionResult Contact(){

        return View();
    }

    public IActionResult Privacy()
    {   
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
