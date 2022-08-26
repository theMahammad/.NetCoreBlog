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
using System.Text;

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
        [DebuggerStepThrough]
        public string TurnToAscii(String str){
            
            StringBuilder sb = new StringBuilder(str.ToLower().Replace(" ","_"));
            
            var characters = new Dictionary<char,char>(){
                    {'ə','e'},
                    {'ü','u'},
                    {'ç','c'},
                    {'ş','s'},
                    {'ı','i'},
                    {'ö','o'},
                    {'ğ','g'}
            }; 
            for(var ch = 0 ; ch < sb.Length ; ch++){

                foreach(var key in characters.Keys){
                    if(sb[ch]==key){
                        sb[ch]=characters[key];
                    }
                }           
            }
            return sb.ToString();
        }
        [Route("HomePage")]
        public IActionResult Index()
        {
           var blogs = _context.Blogs.ToList();
           ViewBag.context = _context;
           Console.WriteLine(TurnToAscii("Salam Mən Gəldim"));
           
           
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
                var fileName =TurnToAscii($"{DateTime.Now:MMddHHmmss}_{blog.Title}.{file.FileName.Split(".").Last()}");
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
        [Route("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(int Id){
            
            try{
            var selectedBlog = await _context.Blogs.FindAsync(Id);
            _context.Remove(selectedBlog);
            await _context.SaveChangesAsync();
            return this.Ok("Bloq silindi");
            
            }catch{
                    return this.BadRequest("Xəta baş verdi");
            }
            
        }
        [Route("EditBlog")]
        public ActionResult EditBlog(int id){
            Blog selectedBlog = _context.Blogs.Find(id);
            ViewBag.selectedBlog=selectedBlog;
            ViewBag.context=_context;
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [Route("EditBlogConfirming")]
        [HttpPost]
        public async Task<IActionResult> EditBlogConfirming(Blog blogUpdated){
            Blog blog = null;
            try{
                
            if(blogUpdated != null){
          
            blog =  _context.Blogs.FirstOrDefault(x=> x.Id==blogUpdated.Id); 
            blog.CategoryId=blogUpdated.CategoryId;
            blog.Content=blogUpdated.Content;
            blog.Title=blogUpdated.Title;
            blog.Subtitle=blogUpdated.Subtitle;
                  if(Request.Form.Files.Count!=0){
                 var files = Request.Form.Files;
                var file = files.First();
            // File Save Operation
                string savePath = Path.Combine("C:","MyBlog","MyBlog","wwwroot","img");
                var fileName = TurnToAscii($"{DateTime.Now:MMddHHmmss}_{blogUpdated.Title}.{file.FileName.Split(".").Last()}");
                var fileUrl = Path.Combine(savePath,fileName);
                using ( var fileStream = new FileStream(fileUrl,FileMode.Create)){
                        await file.CopyToAsync(fileStream);
                        
                }
                blog.ImagePath=fileName;
                }
            

           var foo = _context.Update(blog);
            
            }
            
            
            
            _context.SaveChanges();
            return this.Ok(blog);
            }catch(Exception e){
                
                return this.BadRequest(Json(e));
            }
            
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
        }
    }
}