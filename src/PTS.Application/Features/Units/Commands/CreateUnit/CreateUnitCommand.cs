using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Units.Commands.CreateUnit;

[RequirePermission(Permissions.Units.Create)]
public sealed record CreateUnitCommand(
    string NameEn,
    string NameNp,
    string Abbreviation,
    Guid UnitCategoryId)
    : ICommand<Guid>;
