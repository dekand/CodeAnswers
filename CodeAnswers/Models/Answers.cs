using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Answers
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [Display(Name = "Publication Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }
        public bool Accepted { get; set; }

        public int Rating { get; set; }
        //один-ко-многим (Users-Answers)
        public int? AuthorId { get; set; }
        public Users? User { get; set; }
        //один-ко-многим (Questions-Answers)
        public int QuestionId { get; set; }
        public Questions? Question { get; set; }
        //один-ко-многим (Answers-AnswersRating)
        public List<AnswersRating> AnswerRatings { get; set; } = new();
    }
}
