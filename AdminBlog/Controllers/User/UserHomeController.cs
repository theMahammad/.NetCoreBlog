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

    public IActionResult Index()
    {
        var blogs=_context.Blogs.ToList();
        ViewBag.context= _context;
        
        return View(blogs);
    
    }
    [Route("Post")]
      public IActionResult Post(){

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
