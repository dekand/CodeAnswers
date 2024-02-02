using CodeAnswers.Models;
namespace CodeAnswers.ViewModels
{
    public class TagDetailsViewModel
    {
        public Tags Tag { get; set; } = null!;
        public List<Questions> Questions { get; set; } = null!;
    }
}
