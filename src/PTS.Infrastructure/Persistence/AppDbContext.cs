
using Microsoft.EntityFrameworkCore;
using PTS.Domain.Entities.Materials;
using PTS.Domain.Entities;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence;

public class AppDbContext(
    DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<LocalAdmin> LocalAdmins => Set<LocalAdmin>();
    public DbSet<Factory> Factories => Set<Factory>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Municipality> Municipalities => Set<Municipality>();
    public DbSet<Ward> Wards => Set<Ward>();

    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<UnitCategory> UnitCategories => Set<UnitCategory>();
    public DbSet<MaterialCategory> MaterialCategories => Set<MaterialCategory>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<MaterialUnit> MaterialUnits => Set<MaterialUnit>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Finds every configuration class in this project automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
