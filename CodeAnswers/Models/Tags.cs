using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Tags
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\W]+?$"), StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Questions> Question { get; set; } = new();
    }
}
