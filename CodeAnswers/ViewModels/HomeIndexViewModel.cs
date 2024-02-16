using CodeAnswers.Models;
namespace CodeAnswers.ViewModels
{
    public class HomeIndexViewModel
    {
        public int NotAnswered { get; set; }
        public List<Questions> Questions { get; set; } = null!;
        public PageViewModel PageViewModel { get; set; } = null!;
    }
}
