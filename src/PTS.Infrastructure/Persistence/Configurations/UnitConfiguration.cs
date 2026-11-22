using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Configurations;

public sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(unit => unit.Id);

        builder.Property(unit => unit.NameEn).IsRequired().HasMaxLength(100);
        builder.Property(unit => unit.NameNp).IsRequired().HasMaxLength(100);
        builder.Property(unit => unit.Abbreviation).IsRequired().HasMaxLength(20);
        builder.Property(unit => unit.CreatedAt).IsRequired();

        builder.HasIndex(unit => new { unit.UnitCategoryId, unit.NameEn }).IsUnique();
        builder.HasIndex(unit => new { unit.UnitCategoryId, unit.Abbreviation }).IsUnique();

        builder.HasOne(unit => unit.UnitCategory)
            .WithMany(category => category.Units)
            .HasForeignKey(unit => unit.UnitCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
