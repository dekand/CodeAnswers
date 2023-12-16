using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Questions
    {
        public int Id { get; set; }
        [Required]
        public int IdAuthor { get; set; }

        [StringLength(255, MinimumLength = 5), Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Publication Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Modified Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }

        public int Rating { get; set; }
    }
}
