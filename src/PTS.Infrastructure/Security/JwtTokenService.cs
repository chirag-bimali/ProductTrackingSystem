using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PTS.Application.Abstractions.Security;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Security;

public sealed class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    public GeneratedToken GenerateAccessToken(User user, IEnumerable<string> roles)
    {
        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var signingKey = configuration["Jwt:SigningKey"]!;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        claims.AddRange(roles.Select(role => new Claim("role", role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddDays(15);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new GeneratedToken(serializedToken, expiresAt);
    }
}
