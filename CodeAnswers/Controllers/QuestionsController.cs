using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeAnswers.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Questions.Include(q => q.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new QuestionDetailsData();
            var question = await _context.Questions
                .Include(q => q.User)
                .ThenInclude(c => c.Image)
                .Include(a => a.Answer)
                .Include(t => t.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            viewModel.Question = question;
            viewModel.Answers = await _context.Answers
            .Include(q => q.User)
            .ThenInclude(c => c.Image)
            .Include(a => a.Question)
            .Where(a => a.QuestionId == id)
            .OrderByDescending(c => c.Rating)
            .ThenBy(c => c.PublicationDate)
            .ToListAsync();

            return View(viewModel);
        }
        //POST Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("Description")] Answers answer, int id)
        {
            if (ModelState.IsValid)
            {
                answer.AuthorId = _context.Users.FirstOrDefault(u => u.Name == User.FindFirstValue(ClaimTypes.Name)).Id;
                answer.QuestionId = id;
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details));
            }
            return RedirectToAction(nameof(Details));
        }
        // GET: Questions/Create
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            ViewData["tagsList"] = new SelectList(_context.Tags, "Id", "Name");

            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionTitle,QuestionDescription,TagsId")] QuestionCreateData model)
        {
            if (ModelState.IsValid)
            {
                var question = new Questions() { Title = model.QuestionTitle, Description = model.QuestionDescription };

                var users = await _context.Users
                        .ToListAsync();
                int authorId = users.Find(u => u.Name == User.FindFirstValue(ClaimTypes.Name)).Id;
                if (authorId == 0) { return NotFound(); }
                question.AuthorId = authorId;
                List<Tags> tagsList = await (from tag in _context.Tags
                                             select tag).ToListAsync();

                foreach (var tagId in model.TagsId)
                {
                    question.Tag.Add(tagsList.FirstOrDefault(c => c.Id == tagId));
                }
                _context.Add(question);

                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            ViewData["tagsList"] = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email", questions.AuthorId);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PublicationDate,ModifiedDate,AuthorId,Rating,Answered")] Questions questions)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    questions.ModifiedDate=DateTime.Now;
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/Questions/Details/{id}");
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Email", questions.AuthorId);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions != null)
            {
                _context.Questions.Remove(questions);
            }

            await _context.SaveChangesAsync();
            return Redirect("/Home/Index");
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
