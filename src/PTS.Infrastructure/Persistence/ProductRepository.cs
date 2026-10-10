
using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence;

public sealed class ProductRepository(
    AppDbContext dbContext) : IProductRepository
{
    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken)
    {
        await dbContext.Products.AddAsync(
            product,
            cancellationToken);

        await dbContext.SaveChangesAsync(
            cancellationToken);
    }
}