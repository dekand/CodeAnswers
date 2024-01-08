namespace CodeAnswers.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        //один-к-одному (Images-Users)
        public int UserId { get; set; }
        public Users? User { get; set; }
    }
}
