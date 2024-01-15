namespace CodeAnswers.Models
{
    public class AnswersRating
    {
        public int Id { get; set; }
        public bool Likes { get; set; }
        public bool Dislikes { get; set; }

        //один-ко-многим
        public int UserId { get; set; }
        public Users User { get; set; }
        //один-ко-многим
        public int AnswerId { get; set; }
        public Answers Answer { get; set; }
    }
}
