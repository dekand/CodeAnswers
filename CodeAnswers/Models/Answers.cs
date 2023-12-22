using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Answers
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }

        [Display(Name = "Publication Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        public int Rating { get; set; }
    }
}
