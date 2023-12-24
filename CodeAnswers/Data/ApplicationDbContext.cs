using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodeAnswers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CodeAnswers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Tags> Tags { get; set; } = default!;
        public DbSet<Questions> Questions { get; set; } = default!;
        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Answers> Answers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnswersConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new TagsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }
    }
}
