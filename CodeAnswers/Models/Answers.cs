using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Answers
    {
        public int Id { get; set; }
        [Required]
        public int IdQuestion { get; set; }
        public int IdAuthor { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$"), Required]
        public string Description { get; set; }

        [Display(Name = "Publication Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        public int Rating { get; set; }
    }
}
