using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            var question = await _context.Questions.FirstOrDefaultAsync(c => c.Id == QuestionId);
            if (qLike != null)
            {
                if (await _context.QuestionsRating.ContainsAsync(qLike) && question != null)
                {
                    var lEntity = await _context.QuestionsRating.FirstOrDefaultAsync(c => c == qLike);
                    if (lEntity.Dislikes != lEntity.Likes && lEntity.Dislikes == true)
                    {
                        lEntity.Likes = true;
                        lEntity.Dislikes = false;
                        question.Rating += 2;
                    }
                    else
                    {
                        if (lEntity.Likes == true)
                        {
                            lEntity.Likes = false;
                            question.Rating -= 1;
                        }
                        else
                        {
                            _context.QuestionsRating.Remove(lEntity);
                        }
                    }
                }
                else
                {
                    question.Rating += 1;
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
            var question = await _context.Questions.FirstOrDefaultAsync(c => c.Id == QuestionId);
            if (qDislike != null)
            {
                if (await _context.QuestionsRating.ContainsAsync(qDislike) && question != null)
                {
                    var dEntity = await _context.QuestionsRating.FirstOrDefaultAsync(c => c == qDislike);
                    if (dEntity.Dislikes != dEntity.Likes && dEntity.Likes == true)
                    {
                        dEntity.Likes = false;
                        dEntity.Dislikes = true;
                        question.Rating -= 2;
                    }
                    else
                    {
                        if (dEntity.Dislikes == true)
                        {
                            dEntity.Dislikes = false;
                            question.Rating += 1;
                        }
                        else
                        {
                            _context.QuestionsRating.Remove(dEntity);
                        }
                    }
                }
                else
                {
                    question.Rating -= 1;
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
