using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieBlog.Data;
using MovieBlog.Models;

namespace MovieBlog.Controllers
{
    public class MyBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyBlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyBlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyBlog.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,Heading,Content,IsPublished,CreatedDate")] MyBlog myBlog)
        {
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

        private bool MyBlogExists(int id)
        {
            return _context.MyBlog.Any(e => e.Id == id);
        }
    }
}
