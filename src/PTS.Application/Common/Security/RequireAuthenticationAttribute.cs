namespace PTS.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RequireNoAuthenticationAttribute() : Attribute { }
