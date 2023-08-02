using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDo_Web_APi.DTOs.Auth;
using ToDo_Web_APi.Models;

namespace ToDo_Web_APi.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="roleManager"></param>
    public AuthController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request"></param>
    /// <returns>New token</returns>
    [HttpPost("email")]
    public async Task<ActionResult<AuthTokenDto>> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized();
        }

        var canSignIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!canSignIn.Succeeded)
        {
            return Unauthorized();
        }

        var role = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", role))

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
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("Super Hard Secure Key"));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "https://localhost:7137",
            audience: "https://localhost:7137", expires: DateTime.UtcNow.AddMinutes(3), claims: claims,
            signingCredentials: signingCredentials);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = Guid.NewGuid().ToString("N").ToLower();
        await _userManager.UpdateAsync(user);

        return new AuthTokenDto
        {
            AccessToken = tokenValue,
            RefreshToken = refreshToken
        };
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public async Task<ActionResult<AuthTokenDto>> Refresh(
        [FromBody] RefreshTokenRequest request)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
        if (user is null)
        {
            return Unauthorized();
        }

        var role = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", role))
        }.Concat(userClaims);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("Super Hard Secure Key"));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "https://localhost:7137",
            audience: "https://localhost:7137", expires: DateTime.UtcNow.AddMinutes(3), claims: claims,
            signingCredentials: signingCredentials);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = user.RefreshToken;
        await _userManager.UpdateAsync(user);

        return new AuthTokenDto
        {
            AccessToken = tokenValue,
            RefreshToken = refreshToken
        };
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<AuthTokenDto>> Register([FromBody] RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            return Conflict("User with same email already exists");
        }

        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            RefreshToken = Guid.NewGuid().ToString("N").ToLower()
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var role = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        // Copy paste from Refresh
        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", role))
        }.Concat(userClaims);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("Super Hard Secure Key"));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "https://localhost:7137",
            audience: "https://localhost:7137", expires: DateTime.UtcNow.AddMinutes(3), claims: claims,
            signingCredentials: signingCredentials);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = user.RefreshToken;
        await _userManager.UpdateAsync(user);

        return new AuthTokenDto
        {
            AccessToken = tokenValue,
            RefreshToken = refreshToken
        };
    }
}