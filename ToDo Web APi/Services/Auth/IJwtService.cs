using System.Security.Claims;

namespace ToDo_Web_APi.Services.Auth;

/// <summary>
/// IJwtService
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// GenerateSecurityToken
    /// </summary>
    /// <param name="email"></param>
    /// <param name="roles"></param>
    /// <param name="userClaims"></param>
    /// <returns></returns>
    string GenerateSecurityToken(string email, 
        IEnumerable<string> roles, 
        IEnumerable<Claim> userClaims);
}