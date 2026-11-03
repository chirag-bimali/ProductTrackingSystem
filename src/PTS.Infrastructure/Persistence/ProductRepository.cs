using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;
using PTS.Infrastructure.Persistence.Repositories;

namespace PTS.Infrastructure.Persistence;

public sealed class ProductRepository(AppDbContext dbContext)
    : Repository<Product>(dbContext),
        IProductRepository { }
