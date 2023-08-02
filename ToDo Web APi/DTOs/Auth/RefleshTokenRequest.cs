namespace ToDo_Web_APi.DTOs.Auth;

/// <summary>
/// RefreshTokenRequest
/// </summary>
public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}