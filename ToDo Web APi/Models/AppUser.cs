using Microsoft.AspNetCore.Identity;

namespace ToDo_Web_APi.Models;

/// <summary>
/// AppUser inherits from IdentityUser
/// </summary>
public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
}