using CodeAnswers.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.ViewModels
{
    public class QuestionCreateData
    {
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public int[] TagsId { get; set; }
    }
}
