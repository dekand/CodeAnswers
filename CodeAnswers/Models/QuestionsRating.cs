namespace CodeAnswers.Models
{
    public class QuestionsRating
    {
        public bool Likes { get; set; }
        public bool Dislikes { get; set; }

        //один-ко-многим
        public int UserId { get; set; }
        public Users? User { get; set; }
        //один-ко-многим
        public int QuestionId { get; set; }
        public Questions? Question { get; set; }
    }
}
