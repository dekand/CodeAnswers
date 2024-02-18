using CodeAnswers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            //один-ко-многим (Answers-AnswersRating)
            builder.HasMany(c => c.AnswerRatings)
                .WithOne(c => c.Answer)
                .HasForeignKey(k => k.AnswerId);
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
               .HasForeignKey(u => u.AuthorId);
            //один-ко-многим (Questions-Answers)
            builder
                .HasMany(c => c.Answer)
                .WithOne(s => s.Question)
                .HasForeignKey(u => u.QuestionId);
            //один-ко-многим (Questions-QuestionsRating)
            builder.HasMany(c => c.QuestionRatings)
                .WithOne(c => c.Question)
                .HasForeignKey(k => k.QuestionId);
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
            //builder.Property(p => p.Reputation).HasColumnName("reputation").IsRequired()
            //    .HasComputedColumnSql("[dbo].[Fun_ReputationCalc]([id])");
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
            //один-ко-многим (Users-AnswersRating)
            builder.HasMany(c => c.AnswerRatings)
                .WithOne(c => c.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            //один-ко-многим (Users-QuestionsRating)
            builder.HasMany(c => c.QuestionRatings)
                .WithOne(c => c.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            //один-к-одному (Ratings-Users)
            builder
                .HasOne(c => c.Rating)
                .WithOne(s => s.User)
                .HasForeignKey<Ratings>(u => u.UserId);
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
    public class AnswersRatingConfiguration : IEntityTypeConfiguration<AnswersRating>
    {
        public void Configure(EntityTypeBuilder<AnswersRating> builder)
        {
            builder.Property(p => p.Likes).HasColumnName("likes")
                .HasDefaultValue(false);
            builder.Property(p => p.Dislikes).HasColumnName("dislikes")
                .HasDefaultValue(false);
            builder.Property(p => p.AnswerId).HasColumnName("answer_id").IsRequired();
            builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();

            //один-ко-многим (Users-QuestionsRating)
            builder.HasOne(c => c.User)
                .WithMany(c => c.AnswerRatings)
                .HasForeignKey(k => k.UserId);
            //один-ко-многим (Answers-AnswersRating)
            builder.HasOne(c => c.Answer)
                .WithMany(c => c.AnswerRatings)
                .HasForeignKey(k => k.AnswerId);
            builder.HasKey(b => new { b.AnswerId, b.UserId });

        }
    }
    public class QuestionsRatingConfiguration : IEntityTypeConfiguration<QuestionsRating>
    {
        public void Configure(EntityTypeBuilder<QuestionsRating> builder)
        {
            builder.Property(p => p.Likes).HasColumnName("likes")
                .HasDefaultValue(false);
            builder.Property(p => p.Dislikes).HasColumnName("dislikes")
                .HasDefaultValue(false);
            builder.Property(p => p.QuestionId).HasColumnName("question_id").IsRequired();
            builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
            //один-ко-многим (Users-QuestionsRating)
            builder.HasOne(c => c.User)
                .WithMany(c => c.QuestionRatings)
                .HasForeignKey(k => k.UserId);
            //один-ко-многим (Questions-QuestionsRating)
            builder.HasOne(c => c.Question)
                .WithMany(c => c.QuestionRatings)
                .HasForeignKey(k => k.QuestionId);
            builder.HasKey(b => new { b.QuestionId, b.UserId });
        }
    }
    public class RatingsConfiguration : IEntityTypeConfiguration<Ratings>
    {
        public void Configure(EntityTypeBuilder<Ratings> builder)
        {
            builder.Property(p => p.QuestionsRating).HasColumnName("questions_rating")
                .HasComputedColumnSql("[dbo].[Fun_QuestionsRatingCalc]([user_id])");
            builder.Property(p => p.AnswersRating).HasColumnName("answers_rating")
                .HasComputedColumnSql("[dbo].[Fun_AnswersRatingCalc]([user_id])");
            builder.Property(p => p.Total).HasColumnName("total")
                .HasComputedColumnSql("[dbo].[Fun_TotalRatingCalc]([user_id])");
            builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
            builder.HasKey(k => k.UserId);
            //один к одному (Ratings-Users)
            builder.HasOne(c => c.User)
                .WithOne(c => c.Rating)
                .HasForeignKey<Ratings>(k => k.UserId);
        }
    }
}