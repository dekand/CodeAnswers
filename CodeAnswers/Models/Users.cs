using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CodeAnswers.Models
{
    public class Users
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress), StringLength(100, MinimumLength = 3)]
        public string Email { get; set; }

        [Display(Name = "Registration Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        public int Reputation { get; set; }
        public string? About { get; set; }

        [StringLength(255, MinimumLength = 3)]
        public string? Location { get; set; }

        [DataType(DataType.Url),StringLength(255, MinimumLength = 3)]
        public string? LinkSocial { get; set; }

        [DataType(DataType.Url), StringLength(255, MinimumLength = 3)]
        public string? LinkGithub { get; set; }

        //один-ко-многим (Users-Questions)
        public List<Questions> Question { get; set; } = new();
        //один-ко-многим (Users-Answers)
        public List<Answers> Answer { get; set; } = new();
        //один-к-одному (Images-Users)
        public Images? Image {  get; set; }
    }
}
