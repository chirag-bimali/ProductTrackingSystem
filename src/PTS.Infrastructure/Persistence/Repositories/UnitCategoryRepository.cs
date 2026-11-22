using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Repositories;

public sealed class UnitCategoryRepository(AppDbContext db) : Repository<UnitCategory>(db), IUnitCategoryRepository
{
}
