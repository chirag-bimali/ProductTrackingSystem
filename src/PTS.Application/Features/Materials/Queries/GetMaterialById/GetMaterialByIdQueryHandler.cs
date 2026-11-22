using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using PTS.Application.Features.Materials.Queries.GetAllMaterials;

namespace PTS.Application.Features.Materials.Queries.GetMaterialById;

public sealed class GetMaterialByIdQueryHandler(IMaterialRepository materialRepository)
    : IQueryHandler<GetMaterialByIdQuery, MaterialResult>
{
    public async Task<Result<MaterialResult>> Handle(
        GetMaterialByIdQuery request,
        CancellationToken cancellationToken)
    {
        var material = await materialRepository.GetByIdWithDetailsAsync(
            request.MaterialId,
            cancellationToken);

        if (material is null)
            return Error.NotFound("materials.not_found", "The material was not found.");

        return MaterialResult.FromEntity(material);
    }
}
