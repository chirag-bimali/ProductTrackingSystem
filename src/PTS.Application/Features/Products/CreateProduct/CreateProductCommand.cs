using PTS.Application.Abstractions.Messaging;

namespace PTS.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : ICommand<Guid>;
