using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeAnswers.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 0)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users' is null.");
            }
            var users = from m in _context.Users
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name!.Contains(searchString));
            }

            var viewModel = new UsersIndexViewModel();
            pageSize = pageSize == 0 ? 24 : pageSize;   // count of elements on page
            var count = await users.CountAsync();
            viewModel.PageViewModel = new PageViewModel(count, page, pageSize);
            viewModel.Users = await users
                .Skip((page - 1) * pageSize)//paging
                .Take(pageSize)             //paging
                .Include(c => c.Image)
                .Include(c => c.Rating)
                .OrderByDescending(o => o.Rating.Total)
                .ThenBy(o => o.Name)
                .ToListAsync();
            return View(viewModel);
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(c => c.Image)
                .Include(c => c.Answer)
                .Include(c => c.Rating)
                .Include(c => c.Question)
                .ThenInclude(c => c.Answer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegistrationDate,Reputation,Location,LinkSocial,LinkGithub")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationDate,Reputation,Location,LinkSocial,LinkGithub")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UsersExists(users.Id))
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
            return View(users);
        }

        // GET: Users/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UsersExists(int id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }
    }
}
