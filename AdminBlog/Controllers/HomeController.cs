using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using Microsoft.EntityFrameworkCore;
namespace AdminBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;
    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> AddCategory(Category category){
        if(category.Id ==0){
        await _context.AddAsync(category);
        }else{
            _context.Update(category);
        }
        
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Category));
    }
  public async Task<IActionResult> CategoryDetails(int? id){
    Category category = await _context.Categories.FindAsync(id);
    return Json(category);
  }
     public async Task<IActionResult> DeleteCategory(int? id){
        Category category = await _context.Categories.FindAsync(id);
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Category));
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
