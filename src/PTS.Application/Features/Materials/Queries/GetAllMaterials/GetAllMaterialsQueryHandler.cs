using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Materials.Queries.GetAllMaterials;

public sealed class GetAllMaterialsQueryHandler(IMaterialRepository materialRepository)
    : IQueryHandler<GetAllMaterialsQuery, List<MaterialResult>>
{
    public async Task<Result<List<MaterialResult>>> Handle(
        GetAllMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        var materials = await materialRepository.GetAllWithDetailsAsync(cancellationToken);

        return materials.Select(MaterialResult.FromEntity).ToList();
    }
}
