using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagerDataAccess.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;

namespace TaskManagerWeb.Services.JwtService;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserMail),
            new Claim(ClaimTypes.Name, user.IsUsed.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:Token")));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
            audience: _configuration.GetValue<string>("JwtSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}