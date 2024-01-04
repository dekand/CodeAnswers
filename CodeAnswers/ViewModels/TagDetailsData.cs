using CodeAnswers.Models;
namespace CodeAnswers.ViewModels
{
    public class TagDetailsData
    {
        public Tags Tag { get; set; } = null!;
        public List<Questions> Questions { get; set; } = null!;
    }
}
