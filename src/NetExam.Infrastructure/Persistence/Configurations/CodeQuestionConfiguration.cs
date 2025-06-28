using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class CodeQuestionConfiguration : IEntityTypeConfiguration<CodeQuestion>
{
    public void Configure(EntityTypeBuilder<CodeQuestion> builder)
    {
        builder.ToTable("CodeQuestions");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Title).IsRequired().HasMaxLength(200);
        builder.Property(q => q.Description).HasMaxLength(5000);
        builder.Property(q => q.FunctionName).IsRequired();
    }
}