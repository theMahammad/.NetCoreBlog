using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBlog.Models
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public bool IsReaded { get; set; }
    }
}