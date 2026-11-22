using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;
using PTS.Application.Features.Units.Queries.GetAllUnits;

namespace PTS.Application.Features.Units.Queries.GetUnitById;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetUnitByIdQuery(Guid UnitId) : IQuery<UnitResult>;
