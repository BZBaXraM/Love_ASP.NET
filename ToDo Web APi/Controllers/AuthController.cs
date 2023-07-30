using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ToDo_Web_APi.DTOs.Auth;

namespace ToDo_Web_APi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthTokenDto>> Login([FromBody] LoginRequest request)
    {
        if (request is not { Login: "admin", Password: "admin" })
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, "admin"),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")
        };
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("Super hard secure key"));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "https://localhost:7137",
            audience: "https://localhost:7137", expires: DateTime.UtcNow.AddMinutes(30), claims: claims,
            signingCredentials: signingCredentials);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthTokenDto
        {
            Token = tokenValue
        };
    }
}