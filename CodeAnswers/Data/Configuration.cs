using CodeAnswers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CodeAnswers.Data
{
    public class AnswersConfiguration : IEntityTypeConfiguration<Answers>
    {
        public void Configure(EntityTypeBuilder<Answers> builder)
        {
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.QuestionId).HasColumnName("question_id").IsRequired();
            builder.Property(p => p.AuthorId).HasColumnName("author_id").IsRequired();
            builder.Property(p => p.PublicationDate).HasColumnName("publication_date").IsRequired()
                .HasDefaultValueSql("getdate()");
            builder.Property(p => p.Description).HasColumnName("description").IsRequired();
            builder.Property(p => p.Rating).HasColumnName("rating").IsRequired()
                .HasDefaultValue(0);
            builder.Property(p => p.Accepted).HasColumnName("accepted").IsRequired()
                .HasDefaultValue(false);
            //один-ко-многим (Users-Answers)
            builder
               .HasOne(c => c.User)
               .WithMany(s => s.Answer)
               .HasForeignKey(u => u.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);
            //один-ко-многим (Questions-Answers)
            builder
                .HasOne(c => c.Question)
                .WithMany(s => s.Answer)
                .HasForeignKey(u => u.QuestionId);
        }
    }

    public class QuestionsConfiguration : IEntityTypeConfiguration<Questions>
    {
        public void Configure(EntityTypeBuilder<Questions> builder)
        {
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.AuthorId).HasColumnName("author_id").IsRequired();
            builder.Property(p => p.Title).HasColumnName("title").IsRequired();
            builder.Property(p => p.Description).HasColumnName("description").IsRequired();
            builder.Property(p => p.PublicationDate).HasColumnName("publication_date").IsRequired()
                .HasDefaultValueSql("getdate()");
            builder.Property(p => p.ModifiedDate).HasColumnName("modified_date");
            builder.Property(p => p.Rating).HasColumnName("rating")
                .HasDefaultValue(0);
            builder.Property(p => p.Answered).HasColumnName("answered").IsRequired()
                .HasDefaultValue(false);
            //многие-ко-многим (Questions-Tags)
            builder
               .HasMany(c => c.Tag)
               .WithMany(s => s.Question)
               .UsingEntity(j => j.ToTable("QuestionsTags"));
            //один-ко-многим (Users-Questions)
            builder
               .HasOne(c => c.User)
               .WithMany(s => s.Question)
               .HasForeignKey(u=>u.AuthorId);
            //один-ко-многим (Questions-Answers)
            builder
                .HasMany(c => c.Answer)
                .WithOne(s => s.Question)
                .HasForeignKey(u => u.QuestionId);
        }
    }

    public class TagsConfiguration : IEntityTypeConfiguration<Tags>
    {
        public void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).HasColumnName("name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("description")
            .HasDefaultValue("none");
            //многие-ко-многим (Questions-Tags)
            builder
               .HasMany(c => c.Question)
               .WithMany(s => s.Tag)
               .UsingEntity(j => j.ToTable("QuestionsTags"));
        }
    }

    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).HasColumnName("name").IsRequired();
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(p => p.Email).HasColumnName("email").IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(p => p.RegistrationDate).HasColumnName("registration_date").IsRequired()
                .HasDefaultValueSql("getdate()");
            //СДЕЛАТЬ СТОЛБЕЦ ВЫЧИСЛЯЕМЫМ = Questions.rating+Answers.rating
            builder.Property(p => p.Reputation).HasColumnName("reputation").IsRequired()
                .HasDefaultValue(0);
            builder.Property(p => p.Location).HasColumnName("location");
            builder.Property(p => p.LinkSocial).HasColumnName("link_social");
            builder.Property(p => p.LinkGithub).HasColumnName("link_github");
            builder.Property(p => p.About).HasColumnName("about");
            //один-ко-многим (Users-Questions)
            builder
               .HasMany(c => c.Question)
               .WithOne(s => s.User)
               .HasForeignKey(u => u.AuthorId);
            //один-ко-многим (Users-Answers)
            builder
               .HasMany(c => c.Answer)
               .WithOne(s => s.User)
               .HasForeignKey(u => u.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);
            //один-к-одному (Images-Users)
            builder
                .HasOne(c => c.Image)
                .WithOne(s => s.User)
                .HasForeignKey<Images>(u => u.UserId);
        }
    }
    public class ImagesConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).HasColumnName("name").IsRequired()
                .HasDefaultValue("default.jpg");
            builder.Property(p => p.ImagePath).HasColumnName("image_path").IsRequired()
                .HasDefaultValue("/img/default.jpg");
            builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
            //один-к-одному (Images-Users)
            builder
                .HasOne(c => c.User)
                .WithOne(s => s.Image)
                .HasForeignKey<Images>(u => u.UserId);
        }
    }   

}
