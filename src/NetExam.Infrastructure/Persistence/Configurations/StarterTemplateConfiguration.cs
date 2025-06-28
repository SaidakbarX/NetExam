using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class StarterTemplateConfiguration : IEntityTypeConfiguration<StarterTemplate>
{
    public void Configure(EntityTypeBuilder<StarterTemplate> builder)
    {
        builder.ToTable("StarterTemplates");

        builder.HasKey(st => st.Id);
        builder.Property(st => st.Code).IsRequired();

        builder.HasOne(st => st.ProgrammingLanguage)
               .WithMany(pl => pl.Templates)
               .HasForeignKey(st => st.ProgrammingLanguageId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

