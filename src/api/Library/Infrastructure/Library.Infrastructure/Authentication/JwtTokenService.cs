using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.Infrastructure.Authentication;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateClientToken(ApplicationUser applicationUser, string role, Guid libraryCardId)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("user_id", applicationUser.Id.ToString()),
            new Claim("user_role", role),
            new Claim("libraryCard_id", libraryCardId.ToString()),
        };
        
        return GenerateToken(claims);
    }
    
    public string GenerateToken(ApplicationUser user, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("user_id", user.Id.ToString()),
            new Claim("user_role", role)
        };

        return GenerateToken(claims);
    }

    private string GenerateToken(List<Claim> claims)
    {
        string? secret = _configuration.GetSection("Jwt")["Secret"];

        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new Exception("Jwt secret was not configured");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}