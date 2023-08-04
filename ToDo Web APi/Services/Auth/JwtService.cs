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
    public JwtService(JwtConfig config) => _config = config;

    /// <summary>
    /// GenerateSecurityToken
    /// </summary>
    /// <param name="email"></param>
    /// <param name="roles"></param>
    /// <param name="userClaims"></param>
    /// <returns></returns>
    public string GenerateSecurityToken(string email, IEnumerable<string> roles, IEnumerable<Claim> userClaims)
    {
        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", roles))

            #region Without Identity adding claims

            // new Claim("permissions", JsonSerializer.Serialize(new[]
            // {
            //     "Can Test",
            //     "CanDelete",
            //     "CanEdit",
            //     "CanCreate"
            // }))

            #endregion
        }.Concat(userClaims);
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config.Secret));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "https://localhost:7137",
            audience: "https://localhost:7137", expires: DateTime.UtcNow.AddMinutes(_config.Expiration), claims: claims,
            signingCredentials: signingCredentials);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return accessToken;
    }
}