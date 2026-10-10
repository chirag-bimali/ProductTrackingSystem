using MediatR;

namespace PTS.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<Guid>;