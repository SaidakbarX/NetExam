using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetExam.Domain.Entity;

namespace NetExam.Infrastructure.Persistence.Configurations;

public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
    {
        builder.ToTable("ProgrammingLanguages");

        builder.HasKey(pl => pl.Id);
        builder.Property(pl => pl.Name).IsRequired().HasMaxLength(100);
        builder.Property(pl => pl.DisplayName).HasMaxLength(50);
        builder.Property(pl => pl.Version).HasMaxLength(20);
        builder.Property(pl => pl.IsActive).HasDefaultValue(true);
    }
}
