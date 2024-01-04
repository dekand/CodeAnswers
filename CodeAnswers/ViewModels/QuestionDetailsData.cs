using CodeAnswers.Models;

namespace CodeAnswers.ViewModels
{
    public class QuestionDetailsData
    {
        public Questions Question { get; set; } = null!;
        public List<Answers> Answers { get; set; } = null!;

        //add new answer in DB
        public Answers Answer { get; set; } = null!;
    }
}
