using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CodeAnswers.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        [MaxLength(255), MinLength(5)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Publication Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Modified Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate { get; set; }

        public int? Rating { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Tags> Tag { get; set; } = new List<Tags>();
        public List<QuestionTags> QuestionTag { get; set; } = new List<QuestionTags>();
    }
}
