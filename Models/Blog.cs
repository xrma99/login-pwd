using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMylogin.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorAlias { get; set; }
        public string Tag { get; set; }//content tag
        public string Content { get; set; }
    }
}
