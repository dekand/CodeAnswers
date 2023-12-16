using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodeAnswers.Models;

namespace CodeAnswers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CodeAnswers.Models.Tags> Tags { get; set; } = default!;
        public DbSet<CodeAnswers.Models.Questions> Questions { get; set; } = default!;
        public DbSet<CodeAnswers.Models.Users> Users { get; set; } = default!;
    }
}
