using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace AdminBlog.Models
{
    public class BlogContext:DbContext {
        public BlogContext(DbContextOptions<BlogContext> options) :base(options){
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        [DebuggerStepThrough]
       protected override async void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Blog>()
                 .Property(b => b.CreationTime)
                 .HasDefaultValue(DateTime.Now);
            
         }
         
        
    }
}