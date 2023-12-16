using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class QuestionTags
    {
        public int Id { get; set; }
        [Required]
        public int IdTag { get; set; }

        [Required]
        public int IdQuestion { get; set; }
    }
}
