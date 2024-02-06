using CodeAnswers.Data;
using CodeAnswers.Models;
using CodeAnswers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CodeAnswers.Controllers
{
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> QuestionLike(IFormCollection formCollection)
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
                await DeleteMatch();
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
        public async Task<JsonResult> QuestionDislike(IFormCollection formCollection)
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
                await DeleteMatch();
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

        [HttpPost]
        public async Task<JsonResult> AnswerLike(IFormCollection formCollection)
        {
            var UserId = _context.Users.FirstOrDefault(c => c.Name == formCollection["userName"].ToString()).Id;
            var AnswerId = int.Parse(formCollection["answerId"]);
            var aLike = new AnswersRating()
            {
                UserId = UserId,
                AnswerId = AnswerId,
                Likes = true
            };
            var model = new JsonResponseViewModel();
            var answer = await _context.Answers.FirstOrDefaultAsync(c => c.Id == AnswerId);
            if (aLike != null)
            {
                await DeleteMatch(false);
                if (await _context.AnswersRating.ContainsAsync(aLike) && answer != null)
                {
                    var lEntity = await _context.AnswersRating.FirstOrDefaultAsync(c => c == aLike);
                    if (lEntity.Dislikes != lEntity.Likes && lEntity.Dislikes == true)
                    {
                        lEntity.Likes = true;
                        lEntity.Dislikes = false;
                        answer.Rating += 2;
                    }
                    else
                    {
                        if (lEntity.Likes == true)
                        {
                            lEntity.Likes = false;
                            answer.Rating -= 1;
                        }
                        else
                        {
                            _context.AnswersRating.Remove(lEntity);
                        }
                    }
                }
                else
                {
                    answer.Rating += 1;
                    _context.Add(aLike);
                }
                await _context.SaveChangesAsync();

                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(aLike);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> AnswerDislike(IFormCollection formCollection)
        {
            var id = _context.Users.FirstOrDefault(c => c.Name == formCollection["userName"].ToString()).Id;
            var AnswerId = int.Parse(formCollection["answerId"]);
            var qDislike = new AnswersRating()
            {
                UserId = id,
                AnswerId = AnswerId,
                Dislikes = true
            };
            var model = new JsonResponseViewModel();
            var answer = await _context.Answers.FirstOrDefaultAsync(c => c.Id == AnswerId);
            if (qDislike != null)
            {
                await DeleteMatch(false);
                if (await _context.AnswersRating.ContainsAsync(qDislike) && answer != null)
                {
                    var dEntity = await _context.AnswersRating.FirstOrDefaultAsync(c => c == qDislike);
                    if (dEntity.Dislikes != dEntity.Likes && dEntity.Likes == true)
                    {
                        dEntity.Likes = false;
                        dEntity.Dislikes = true;
                        answer.Rating -= 2;
                    }
                    else
                    {
                        if (dEntity.Dislikes == true)
                        {
                            dEntity.Dislikes = false;
                            answer.Rating += 1;
                        }
                        else
                        {
                            _context.AnswersRating.Remove(dEntity);
                        }
                    }
                }
                else
                {
                    answer.Rating -= 1;
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
        
        public async Task DeleteMatch(bool question = true)
        {
            if (question)
            {
                var match = await _context.QuestionsRating.Where(c => c.Likes == c.Dislikes).ToListAsync();
                if (match.Count > 0) { _context.RemoveRange(match); }
            }
            else
            {
                var match = await _context.AnswersRating.Where(c => c.Likes == c.Dislikes).ToListAsync();
                if (match.Count > 0) { _context.RemoveRange(match); }
            }
            await _context.SaveChangesAsync();
        }
    }
}
