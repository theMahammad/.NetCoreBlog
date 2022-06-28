using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;

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
        await _context.AddAsync(category);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Category));
    }
    public async Task<IActionResult> DeleteCategory(int? id){
        Category category = await _context.Categories.FindAsync(id);
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Category));
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Category()
    {
        List<Category> categories = _context.Categories.ToList();
        return View(categories);
    }
    public IActionResult Author(){
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
