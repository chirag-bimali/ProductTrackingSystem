using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Configurations;

public sealed class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(material => material.Id);

        builder.Property(material => material.Code).IsRequired().HasMaxLength(50);
        builder.Property(material => material.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(material => material.NameNp).IsRequired().HasMaxLength(200);
        builder.Property(material => material.Description).HasMaxLength(1000);
        builder.Property(material => material.MinimumStockLevel).HasPrecision(18, 6);
        builder.Property(material => material.ReorderLevel).HasPrecision(18, 6);
        builder.Property(material => material.CreatedAt).IsRequired();

        builder.HasIndex(material => material.Code).IsUnique();
        builder.HasIndex(material => material.NameEn).IsUnique();
        builder.HasIndex(material => material.MaterialCategoryId);

        builder.HasOne(material => material.MaterialCategory)
            .WithMany(category => category.Materials)
            .HasForeignKey(material => material.MaterialCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Inventory persistence is a separate feature. Avoid conventionally creating
        // batch and serial-number tables as part of the material master-data slice.
        builder.Ignore(material => material.Batches);
        builder.Ignore(material => material.SerialNumbers);
    }
}
