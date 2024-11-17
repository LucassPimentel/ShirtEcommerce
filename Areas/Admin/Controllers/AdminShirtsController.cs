using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using sh_rt.Context;
using sh_rt.Models;

namespace sh_rt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminShirtsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminShirtsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminShirts
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Shirts.Include(s => s.Category);
        //    return View(await appDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sort = "Name")
        {
            var result = _context.Shirts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(x => x.Name.Contains(filter));
            }

            var paging = await PagingList.CreateAsync(result, 5, pageIndex, sort, "Name");
            paging.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(paging);
        }

        // GET: Admin/AdminShirts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return View(shirt);
        }

        // GET: Admin/AdminShirts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/AdminShirts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDescription,LongDescription,Price,FrontShirtImageUrl,BehindShirtImageUrl,ImageThumbnailUrl,IsFavoriteShirt,InStock,CategoryId")] Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shirt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", shirt.CategoryId);
            return View(shirt);
        }

        // GET: Admin/AdminShirts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts.FindAsync(id);
            if (shirt == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", shirt.CategoryId);
            return View(shirt);
        }

        // POST: Admin/AdminShirts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortDescription,LongDescription,Price,FrontShirtImageUrl,BehindShirtImageUrl,ImageThumbnailUrl,IsFavoriteShirt,InStock,CategoryId")] Shirt shirt)
        {
            if (id != shirt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shirt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShirtExists(shirt.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", shirt.CategoryId);
            return View(shirt);
        }

        // GET: Admin/AdminShirts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shirt = await _context.Shirts
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shirt == null)
            {
                return NotFound();
            }

            return View(shirt);
        }

        // POST: Admin/AdminShirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shirt = await _context.Shirts.FindAsync(id);
            if (shirt != null)
            {
                _context.Shirts.Remove(shirt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShirtExists(int id)
        {
            return _context.Shirts.Any(e => e.Id == id);
        }
    }
}
