using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Tags
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\W]+?$"), StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Questions> Question { get; set; } = new List<Questions>();
        public List<QuestionTags> QuestionTag { get; set; } = new List<QuestionTags>();

    }
}
