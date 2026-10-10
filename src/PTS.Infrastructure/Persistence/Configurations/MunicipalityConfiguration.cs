
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Domain.Entities;
public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.ToTable("municipalities");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedNever();
        builder.Property(m => m.NameEn).IsRequired().HasMaxLength(150);
        builder.Property(m => m.NameNp).IsRequired().HasMaxLength(150);

        // List<string> Wards becomes a PostgreSQL text[] column automatically

        builder.HasOne(m => m.District)
            .WithMany(d => d.Municipalities)
            .HasForeignKey(m => m.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}