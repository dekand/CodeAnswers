using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeAnswers.Data;
using CodeAnswers.Models;
using System.Threading;

namespace CodeAnswers.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Answers
                .Include(a => a.Question)
                .Include(a => a.User)
                .ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.Question)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Description");
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,PublicationDate,Rating,AuthorId,QuestionId")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Description", answers.QuestionId);
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email", answers.AuthorId);
            return View(answers);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(c=>c.User)
                .Include(c=>c.Question)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (answers == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Description", answers.QuestionId);
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email", answers.AuthorId);
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublicationDate,QuestionId,Rating,AuthorId,Description")] Answers answers)
        {
            if (id != answers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AnswersExists(answers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return Redirect(Url.RouteUrl(new { controller = "Questions", action = "Details", id = answers.QuestionId }));
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.Question)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            if (answers != null)
            {
                _context.Answers.Remove(answers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AnswersExists(int id)
        {
            return await _context.Answers.AnyAsync(e => e.Id == id);
        }
    }
}
