using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Units.Commands.DeleteUnit;

[RequirePermission(Permissions.Units.Delete)]
public sealed record DeleteUnitCommand(Guid UnitId) : ICommand;
