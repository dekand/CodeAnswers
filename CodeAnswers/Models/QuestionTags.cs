using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAnswers.Models
{
    public class QuestionTags
    {
        //public int Id { get; set; }
        [Key]
        public int TagId { get; set; }

        public int QuestionId { get; set; }

        public Tags Tags { get; set; }
        public Questions Questions { get; set; }
    }
}
