using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;

namespace CodeAnswers.Controllers
{
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tags
        public async Task<IActionResult> Index(string searchString, int page =1, int pageSize = 0)
        {

            if (_context.Tags == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tags' is null.");
            }

            var tags = from m in _context.Tags
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                tags = tags.Where(s => s.Name!.Contains(searchString));
            }

            var viewModel = new TagsIndexViewModel();
            pageSize = pageSize == 0 ? 24 : pageSize;   // count of elements on page
            var count = await tags.CountAsync();
            viewModel.PageViewModel = new PageViewModel(count, page, pageSize);
            viewModel.Tags = await tags
                .Skip((page - 1) * pageSize)//paging
                .Take(pageSize)             //paging
                .Include(s => s.Question)
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var viewModel = new TagDetailsViewModel();

            var tag = await _context.Tags
                .Include(q => q.Question)
                .ThenInclude(u=>u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            viewModel.Tag = tag;
            viewModel.Questions = await _context.Questions
                .Include(c => c.User)
                .ThenInclude(u => u.Image)
                .Include(t => t.Tag)
                .Include(a => a.Answer)
                .Where(c=>c.Tag.Contains(tag))
                .OrderByDescending(o => o.Rating)
                .ThenByDescending(o => o.PublicationDate)
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = await _context.Tags.FindAsync(id);
            if (tags == null)
            {
                return NotFound();
            }
            return View(tags);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Tags tags)
        {
            if (id != tags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tags);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TagsExists(tags.Id))
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
            return View(tags);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tags == null)
            {
                return NotFound();
            }

            return View(tags);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tags = await _context.Tags.FindAsync(id);
            if (tags != null)
            {
                _context.Tags.Remove(tags);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TagsExists(int id)
        {
            return await _context.Tags.AnyAsync(e => e.Id == id);
        }
    }
}
