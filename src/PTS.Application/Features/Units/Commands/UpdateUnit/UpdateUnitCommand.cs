using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Units.Commands.UpdateUnit;

[RequirePermission(Permissions.Units.Update)]
public sealed record UpdateUnitCommand(
    Guid UnitId,
    string NameEn,
    string NameNp,
    string Abbreviation,
    Guid UnitCategoryId)
    : ICommand;
