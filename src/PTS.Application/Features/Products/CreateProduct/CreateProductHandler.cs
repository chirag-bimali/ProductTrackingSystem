
using MediatR;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;

namespace PTS.Application.Features.Products.CreateProduct;

public sealed class CreateProductHandler
    : IRequestHandler<CreateProductCommand, Guid>
{
  private readonly IProductRepository _productRepository;

  public CreateProductHandler(
      IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  public async Task<Guid> Handle(
      CreateProductCommand request,
      CancellationToken cancellationToken)
  {
    var product = new Product(
        request.Name,
        request.Price);

    await _productRepository.AddAsync(
        product,
        cancellationToken);

    return product.Id;
  }
}