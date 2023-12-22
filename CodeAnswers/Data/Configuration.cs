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
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(p => p.RegistrationDate).HasColumnName("registration_date").IsRequired()
                .HasDefaultValueSql("getdate()");
            builder.Property(p => p.Reputation).HasColumnName("reputation").IsRequired()
                .HasDefaultValue(0);
            builder.Property(p => p.Location).HasColumnName("location");
            builder.Property(p => p.LinkSocial).HasColumnName("link_social");
            builder.Property(p => p.LinkGithub).HasColumnName("link_github");
        }
    }
    //ПОСЛЕ ТОГО КАК РАЗБЕРУСЬ СО СВЯЗЯМИ МНОГИЕ КО МНОГИМ, ОДИН КО МНОГИМ, ОДИН К ОДНОМУ И ТД..
    //public class QuestionTagsConfiguration : IEntityTypeConfiguration<QuestionTags>
    //{
    //    public void Configure(EntityTypeBuilder<QuestionTags> builder)
    //    {

    //    }
    //}
}
