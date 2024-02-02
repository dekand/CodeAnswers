namespace CodeAnswers.Models
{
    public class Ratings
    {
        public int? QuestionsRating { get; set; }
        public int? AnswersRating { get; set; }
        public int? Total { get; set; }

        //один-к-одному
        public int UserId {  get; set; }
        public Users User { get; set; }   
    }
}
