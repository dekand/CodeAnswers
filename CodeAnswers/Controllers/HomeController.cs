using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.Services;
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

        public async Task<IActionResult> Index(string searchString)
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

            return View(await questions
                .Include(s => s.Tag)
                .Include(a => a.Answer)
                .Include(c => c.User)
                .ThenInclude(u => u.Image)
                .OrderByDescending(o=>o.Rating)
                .ThenByDescending(o=>o.PublicationDate)
                .ToListAsync());
        }

        [HttpGet("{id}/Tags", Name = nameof(GetTag))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Tags>> GetTag(int id)
        {
            var result = _context.Questions.FirstOrDefault(a => a.Id == id);

            if (result == null)
                return BadRequest($"Теги с заданным Id: {id} не существует");

            return result.Tag.ToArray();
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
