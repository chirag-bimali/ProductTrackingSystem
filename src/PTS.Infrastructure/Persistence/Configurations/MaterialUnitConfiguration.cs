using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Configurations;

public sealed class MaterialUnitConfiguration : IEntityTypeConfiguration<MaterialUnit>
{
    public void Configure(EntityTypeBuilder<MaterialUnit> builder)
    {
        builder.HasKey(materialUnit => new { materialUnit.MaterialId, materialUnit.UnitId });

        builder.Property(materialUnit => materialUnit.ConversionFactorToBaseUnit)
            .HasPrecision(24, 12)
            .IsRequired();

        builder.Property(materialUnit => materialUnit.IsBaseUnit).IsRequired();

        builder.HasIndex(materialUnit => materialUnit.MaterialId)
            .IsUnique()
            .HasFilter("\"IsBaseUnit\" = TRUE");

        builder.HasOne(materialUnit => materialUnit.Material)
            .WithMany(material => material.MaterialUnits)
            .HasForeignKey(materialUnit => materialUnit.MaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(materialUnit => materialUnit.Unit)
            .WithMany()
            .HasForeignKey(materialUnit => materialUnit.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(table =>
        {
            table.HasCheckConstraint(
                "CK_MaterialUnits_ConversionFactor_Positive",
                "\"ConversionFactorToBaseUnit\" > 0");
            table.HasCheckConstraint(
                "CK_MaterialUnits_BaseFactor",
                "NOT \"IsBaseUnit\" OR \"ConversionFactorToBaseUnit\" = 1");
        });
    }
}
