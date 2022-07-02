using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace adminblog.Controllers
{
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly BlogContext _context;
        public BlogController(ILogger<BlogController> logger, BlogContext context)
        {
            
            _logger = logger;
            _context = context;
            }
        
        public IActionResult Index()
        {
            ViewBag.Categories = _context.Categories.Select(c=>
                new SelectListItem(){
                    Text = c.Name,
                    Value= c.Id.ToString()
                }
            ).ToList();
            return View();
        }
        [Route("SaveBlog")]
        public async Task<IActionResult> SaveBlog(Blog blog){
            if(blog!=null){
                var file = Request.Form["Title"];
            }

            return Json(false);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
        }
    }
}