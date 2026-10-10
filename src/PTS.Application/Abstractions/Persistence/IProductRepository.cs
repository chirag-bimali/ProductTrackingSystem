
using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface IProductRepository
{
    Task AddAsync(
        Product product,
        CancellationToken cancellationToken);
}