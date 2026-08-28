using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

public class JwtService
{
    public string GenerateToken(User user)
    {
        var key =
            Environment.GetEnvironmentVariable("JWT_KEY")
            ?? throw new Exception("JWT_KEY missing");

        var issuer =
            Environment.GetEnvironmentVariable("JWT_ISSUER");

        var audience =
            Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Role,user.Role?.Name ?? ""),
            new Claim("UserId",user.Id.ToString())
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}