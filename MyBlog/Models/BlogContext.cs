using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MyBlog.Models
{
    public class BlogContext:DbContext {
        public BlogContext(DbContextOptions<BlogContext> options) :base(options){
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
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
        public bool checkSlugRepetition(string slug){
                var blog = this.Blogs.FirstOrDefault(b=>b.Slug==slug);
                if(blog!=null){
                        return true;
                }else{
                    return false;
                }

            
        }
        protected override async void OnModelCreating(ModelBuilder builder){
                    builder.Entity<Blog>().Property(b=> b.CreationTime).HasDefaultValue(DateTime.Now);
                    
        }
        
    }
}