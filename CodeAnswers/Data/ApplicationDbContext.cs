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
        public DbSet<Tags> Tags { get; set; } = default!;
        public DbSet<Questions> Questions { get; set; } = default!;
        public DbSet<QuestionTags> QuestionTags { get; set; } = default!;
        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Answers> Answers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tags>().ToTable("Tags");
            modelBuilder.Entity<Questions>().ToTable("Questions");
            modelBuilder.Entity<QuestionTags>().ToTable("QuestionTags");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Answers>().ToTable("Answers");

            //modelBuilder.Entity<QuestionTags>()
            //    .HasKey(c => new { c.TagId, c.QuestionId });
        }
    }
}
