using CodeAnswers.Models;

namespace CodeAnswers.ViewModels
{
    public class UsersIndexViewModel
    {
        public List<Users> Users { get; set; } = null!;
        public PageViewModel PageViewModel { get; set; } = null!;
    }
}
