using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
{
    public void Configure(EntityTypeBuilder<ExamQuestion> builder)
    {
        builder.ToTable("ExamQuestions");
        builder.HasKey(eq => new { eq.ExamId, eq.CodeQuestionId });

        builder.HasOne(eq => eq.Exam)
               .WithMany(e => e.ExamQuestions)
               .HasForeignKey(eq => eq.ExamId);

        builder.HasOne(eq => eq.CodeQuestion)
               .WithMany(q => q.ExamQuestions)
               .HasForeignKey(eq => eq.CodeQuestionId);
    }
}