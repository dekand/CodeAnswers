using CodeAnswers.Models;

namespace CodeAnswers.ViewModels
{
    public class TagsIndexViewModel
    {
        public List<Tags> Tags { get; set; } = null!;
        public PageViewModel PageViewModel { get; set; } = null!;
    }
}
