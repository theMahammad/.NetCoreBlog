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
        
        [Route("HomePage")]
        public IActionResult Index()
        {
            var blogs = _context.Blogs.ToList();
           ViewBag.context = _context;
            return View(blogs);
        }
        public IActionResult Blog(){

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
        
           if(blog != null){
            var files = Request.Form.Files;
            var file = files.First();
            // File Save Operation
                string savePath = Path.Combine("C:","MyBlog","MyBlog","wwwroot","img");
                var fileName = $"{DateTime.Now:MMddHHmmss}_{blog.Title}.{file.FileName.Split(".").Last()}";
                var fileUrl = Path.Combine(savePath,fileName);
                using ( var fileStream = new FileStream(fileUrl,FileMode.Create)){
                        await file.CopyToAsync(fileStream);
                        
                }
            blog.ImagePath=fileName;
            
            blog.AuthorId = (int) HttpContext.Session.GetInt32("id");
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return this.Ok("Əlavə olundu");
           }

            return Json(false);
        }
        [Route("Aue")]
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
        }
    }
}