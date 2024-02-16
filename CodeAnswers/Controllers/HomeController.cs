using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeAnswers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 0)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Questions' is null.");
            }

            var questions = from m in _context.Questions
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(s => s.Title!.Contains(searchString));
            }

            var viewModel = new HomeIndexViewModel();
            pageSize = pageSize == 0 ? 16 : pageSize;   // count of elements on page
            var count = await questions.CountAsync();
            viewModel.PageViewModel = new PageViewModel(count, page, pageSize);
            viewModel.NotAnswered = await questions.Where(c => c.Answered == false).CountAsync();
            viewModel.Questions = await questions
                .Skip((page - 1) * pageSize)//paging
                .Take(pageSize)             //paging
                .Include(s => s.Tag)
                .Include(a => a.Answer)
                .Include(c => c.User)
                .ThenInclude(u => u.Image)
                .OrderByDescending(o => o.Rating)
                .ThenByDescending(o => o.PublicationDate)
                .ToListAsync();


            return View(viewModel);
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
