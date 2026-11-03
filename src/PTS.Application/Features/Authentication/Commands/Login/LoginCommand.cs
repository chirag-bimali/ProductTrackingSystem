namespace PTS.Application.Features.Authentication.Commands.Login;

public record LoginCommand(string? Email, string? Contact, string Password);
