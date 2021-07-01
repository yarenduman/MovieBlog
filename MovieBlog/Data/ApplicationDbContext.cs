using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyList> MyList { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}
