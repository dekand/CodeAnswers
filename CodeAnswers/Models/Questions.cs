using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CodeAnswers.Models
{
    public class Questions
    {
        public int Id { get; set; }

        [MaxLength(255), MinLength(5)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Publication Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Modified Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate { get; set; }

        public int? Rating { get; set; }
        public bool Answered { get; set; }

        //многие-ко-многим (Questions-Tags)
        public List<Tags> Tag { get; set; } = new();
        //один-ко-многим (Users-Questions)
        public int? AuthorId { get; set; }
        public Users? User { get; set; }
        //один-ко-многим (Questions-Answers)
        public List<Answers> Answer { get; set; } = new();
        //один-ко-многим (Questions-QuestionsRating)
        public List<QuestionsRating> QuestionRatings { get; set; } = new();
    }
}
