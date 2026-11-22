using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Units.Queries.GetAllUnits;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetAllUnitsQuery() : IQuery<List<UnitResult>>;
