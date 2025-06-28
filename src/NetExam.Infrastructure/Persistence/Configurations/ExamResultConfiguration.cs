using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
{
    public void Configure(EntityTypeBuilder<ExamResult> builder)
    {
        builder.ToTable("ExamResults");

        builder.HasKey(er => er.Id);
        builder.Property(er => er.Score).IsRequired();
        builder.Property(er => er.CompletedAt).IsRequired();

        builder.HasOne(er => er.Exam)
               .WithMany(e => e.Results)
               .HasForeignKey(er => er.ExamId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(er => er.User)
               .WithMany()
               .HasForeignKey(er => er.UserId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}

