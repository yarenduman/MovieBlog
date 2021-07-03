using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieBlog.Data;
using MovieBlog.Models;

namespace MovieBlog.Controllers
{
    [Authorize]
    public class MyBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Author> _userManager;

        public MyBlogsController(ApplicationDbContext context, UserManager<Author> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MyBlogs
        public async Task<IActionResult> Index(bool showall = false)
        {
            var Author = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.ShowAll = showall;
            var applicationDbContext = _context.MyBlog.AsQueryable().Where(m => m.AuthorId == Author.Id);
            if (!showall)
            {
                applicationDbContext = applicationDbContext.Where(m => !m.IsPublished);
            }
            applicationDbContext = applicationDbContext.OrderBy(m => m.Heading);
            return View(await applicationDbContext.ToListAsync());
        }

            // GET: MyBlogs/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myBlog = await _context.MyBlog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myBlog == null)
            {
                return NotFound();
            }

            return View(myBlog);
        }

        // GET: MyBlogs/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
 
        public async Task<IActionResult> Create([Bind("Id,Heading,Content,IsPublished,CreatedDate,AuthorId")] MyBlog myBlog)
        {
            var Author = await _userManager.GetUserAsync(HttpContext.User);
            myBlog.AuthorId = Author.Id;

            if (ModelState.IsValid)
            {
                _context.Add(myBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myBlog);
        }

        // GET: MyBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myBlog = await _context.MyBlog.FindAsync(id);
            if (myBlog == null)
            {
                return NotFound();
            }
            return View(myBlog);
        }

        // POST: MyBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Heading,Content,IsPublished,CreatedDate")] MyBlog myBlog)
        {
            if (id != myBlog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyBlogExists(myBlog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(myBlog);
        }

        // GET: MyBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myBlog = await _context.MyBlog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myBlog == null)
            {
                return NotFound();
            }

            return View(myBlog);
        }

        // POST: MyBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myBlog = await _context.MyBlog.FindAsync(id);
            _context.MyBlog.Remove(myBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Published(int id, bool showAll)
        {
            return await ChangeStatus(id, false, showAll);
        }

        public async Task<IActionResult> Unpublished(int id, bool showAll)
        {
            return await ChangeStatus(id, true, showAll);
        }
        private async Task<IActionResult> ChangeStatus(int id, bool status, bool CurrentShowAllValue)
        {
            var MyBlogItem = _context.MyBlog.FirstOrDefault(toPublish => toPublish.Id == id);
            if (MyBlogItem == null)
            {
                return NotFound();
            }
            MyBlogItem.IsPublished = status;
            MyBlogItem.CreatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { showall = CurrentShowAllValue});
        }
        private bool MyBlogExists(int id)
        {
            return _context.MyBlog.Any(e => e.Id == id);
        }
    }
}
