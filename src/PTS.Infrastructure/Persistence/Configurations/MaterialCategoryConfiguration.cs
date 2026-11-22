using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Configurations;

public sealed class MaterialCategoryConfiguration : IEntityTypeConfiguration<MaterialCategory>
{
    public void Configure(EntityTypeBuilder<MaterialCategory> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.NameEn).IsRequired().HasMaxLength(100);
        builder.Property(category => category.NameNp).IsRequired().HasMaxLength(100);
        builder.Property(category => category.Code).HasMaxLength(50);
        builder.Property(category => category.Description).HasMaxLength(500);
        builder.Property(category => category.CreatedAt).IsRequired();

        builder.HasIndex(category => category.NameEn).IsUnique();
        builder.HasIndex(category => category.Code).IsUnique();
        builder.HasIndex(category => category.ParentCategoryId);

        builder.HasOne(category => category.ParentCategory)
            .WithMany(category => category.SubCategories)
            .HasForeignKey(category => category.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
