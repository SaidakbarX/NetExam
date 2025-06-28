using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.ToTable("Exams");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Subject).HasMaxLength(100);
        builder.Property(e => e.CreatedAt).IsRequired();

        builder.HasOne(e => e.CreatedBy)
               .WithMany()
               .HasForeignKey(e => e.CreatedByUserId);
    }
}
