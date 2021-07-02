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
    public class MyListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyLists
        public async Task<IActionResult> Index(bool showall=false)
        {
            var applicationDbContext = _context.MyList.Include(m => m.Genre).AsQueryable();
            if (!showall)
            {
                applicationDbContext = applicationDbContext.Where(m => !m.IsCompleted);
            }
            applicationDbContext = applicationDbContext.OrderBy(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: MyLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myList = await _context.MyList
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myList == null)
            {
                return NotFound();
            }

            return View(myList);
        }

        // GET: MyLists/Create
        public IActionResult Create()
        {
            ViewBag.GenreSelectList = new SelectList(_context.Genre, "Id", "Name");
            return View();
        }

        // POST: MyLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Movie,GenreId,Director,Year,IsCompleted")] MyList myList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", myList.GenreId);
            return View(myList);
        }

        // GET: MyLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myList = await _context.MyList.FindAsync(id);
            if (myList == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", myList.GenreId);
            return View(myList);
        }

        // POST: MyLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Movie,GenreId,Director,Year,IsCompleted")] MyList myList)
        {
            if (id != myList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyListExists(myList.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", myList.GenreId);
            return View(myList);
        }

        // GET: MyLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myList = await _context.MyList
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myList == null)
            {
                return NotFound();
            }

            return View(myList);
        }

        // POST: MyLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myList = await _context.MyList.FindAsync(id);
            _context.MyList.Remove(myList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Watched(int id)
        {
            return await ChangeStatus(id, false);
        }

        public async Task<IActionResult> Unwatched(int id)
        {
            return await ChangeStatus(id, true);
        }
        private async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var MyListItem = _context.MyList.FirstOrDefault(toWatch => toWatch.Id == id);
            if (MyListItem == null)
            {
                return NotFound();
            }
            MyListItem.IsCompleted = status;
            MyListItem.CompletedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    private bool MyListExists(int id)
        {
            return _context.MyList.Any(e => e.Id == id);
        }
    }
}
