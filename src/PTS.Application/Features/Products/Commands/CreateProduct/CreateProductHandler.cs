using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using PTS.Domain.Entities;

namespace PTS.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductHandler(IProductRepository _productRepository) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = new Product(request.Name, request.Price);

        await _productRepository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}
