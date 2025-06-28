using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class CodeSubmissionConfiguration : IEntityTypeConfiguration<CodeSubmission>
{
    public void Configure(EntityTypeBuilder<CodeSubmission> builder)
    {
        builder.ToTable("CodeSubmissions");

        builder.HasKey(cs => cs.Id);
        builder.Property(cs => cs.Code).IsRequired();
        builder.Property(cs => cs.ResultLog);
        builder.Property(cs => cs.SubmittedAt).IsRequired();

        builder.HasOne(cs => cs.User)
               .WithMany(u => u.Submissions)
               .HasForeignKey(cs => cs.UserId);

        builder.HasOne(cs => cs.CodeQuestion)
               .WithMany()
               .HasForeignKey(cs => cs.CodeQuestionId);

        builder.HasOne(cs => cs.ProgrammingLanguage)
               .WithMany()
               .HasForeignKey(cs => cs.ProgrammingLanguageId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
