using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBlog.Models
{
    public class Blog:IDisposable
    {
        public int Id { get; set; }
    
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set;}
        public string ImagePath { get; set; }
        public bool isPublished { get; set; }
        public DateTime CreationTime { get; set; } 
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Category Category { get; set; }
       
        public int CategoryId { get; set; }

         bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    }
}