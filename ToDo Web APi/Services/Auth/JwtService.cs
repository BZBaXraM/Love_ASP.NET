using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDo_Web_APi.Auth;

namespace ToDo_Web_APi.Services.Auth;

/// <summary>
/// JwtService
/// </summary>
public class JwtService : IJwtService
{
    private readonly JwtConfig _config;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="config"></param>
    public JwtService(JwtConfig config)
    {
        _config = config;
    }

    /// <summary>
    /// GenerateSecurityToken
    /// </summary>
    /// <param name="email"></param>
    /// <param name="roles"></param>
    /// <param name="userClaims"></param>
    /// <returns></returns>
    public string GenerateSecurityToken(string id, string email, IEnumerable<string> roles,
        IEnumerable<Claim> userClaims)
    {
        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", roles)),
            new Claim("userId", id)
        }.Concat(userClaims);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config.Secret));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _config.Issuer,
            audience: _config.Audience, expires: DateTime.UtcNow.AddMinutes(_config.Expiration), claims: claims,
            signingCredentials: signingCredentials);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return accessToken;
    }
}