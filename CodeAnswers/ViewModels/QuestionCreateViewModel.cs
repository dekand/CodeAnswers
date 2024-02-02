using CodeAnswers.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.ViewModels
{
    public class QuestionCreateViewModel
    {
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public int[] TagsId { get; set; }
    }
}
