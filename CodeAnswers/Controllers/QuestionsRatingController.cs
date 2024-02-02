using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Principal;

namespace CodeAnswers.Controllers
{
    public class QuestionsRatingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsRatingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Like(IFormCollection formCollection)
        {
            var UserId = _context.Users.FirstOrDefault(c => c.Name == formCollection["userName"].ToString()).Id;
            var QuestionId = int.Parse(formCollection["questionId"]);
            var qLike = new QuestionsRating()
            {
                UserId = UserId,
                QuestionId = QuestionId,
                Likes = true
            };
            var model = new JsonResponseViewModel();
            if (qLike != null)
            {
                if (await _context.QuestionsRating.ContainsAsync(qLike))
                {
                    var lEntity = await _context.QuestionsRating.FirstOrDefaultAsync(c => c == qLike);
                    lEntity.Likes = !lEntity.Likes;
                    lEntity.Dislikes = false;
                    if (lEntity.Dislikes == lEntity.Likes) { _context.QuestionsRating.Remove(lEntity); }
                }
                else
                {
                    _context.Add(qLike);
                }
                await _context.SaveChangesAsync();

                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(qLike);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> Dislike(IFormCollection formCollection)
        {
            var id = _context.Users.FirstOrDefault(c => c.Name == formCollection["userName"].ToString()).Id;
            var QuestionId = int.Parse(formCollection["questionId"]);
            var qDislike = new QuestionsRating()
            {
                UserId = id,
                QuestionId = QuestionId,
                Dislikes = true
            };
            var model = new JsonResponseViewModel();
            if (qDislike != null)
            {
                if (await _context.QuestionsRating.ContainsAsync(qDislike))
                {
                    var dEntity = await _context.QuestionsRating.FirstOrDefaultAsync(c => c == qDislike);
                    dEntity.Dislikes = !dEntity.Dislikes;
                    dEntity.Likes = false;
                    if(dEntity.Dislikes== dEntity.Likes) { _context.QuestionsRating.Remove(dEntity); }
                }
                else
                {
                    _context.Add(qDislike);
                }
                await _context.SaveChangesAsync();

                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(qDislike);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }
    }
}
