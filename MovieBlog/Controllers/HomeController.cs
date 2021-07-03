using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieBlog.Data;
using MovieBlog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBlog.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<Author> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<Author> userManager)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            List<MyBlog> result;

            if (User.Identity.IsAuthenticated)
            {
                var Author = await userManager.GetUserAsync(HttpContext.User);
                var query = dbContext.MyBlog
                    .Where(b => b.AuthorId == Author.Id && b.IsPublished)
                    .OrderBy(b => b.CreatedDate);
                result = await query.ToListAsync();
            }
            else
            {
                result = new List<MyBlog>();
            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
