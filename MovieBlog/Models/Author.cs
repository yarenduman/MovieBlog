using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBlog.Models
{
    public class Author : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<MyList> MyListItem { get; set; }
        public virtual List<MyBlog> MyBlog { get; set; }
    }
}
