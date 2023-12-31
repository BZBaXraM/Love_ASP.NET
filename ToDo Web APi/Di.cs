using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ToDo_Web_APi.Auth;
using ToDo_Web_APi.Data;
using ToDo_Web_APi.Models;
using ToDo_Web_APi.Providers;
using ToDo_Web_APi.Services.Auth;

namespace ToDo_Web_APi;

/// <summary>
/// DI - Dependency Injection
/// </summary>
public static class Di
{
    /// <summary>
    /// AddDbContext
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1"
                , new OpenApiInfo
                {
                    Title = "ToDo",
                    Version = "v2"
                });
            var filePath = Path.Combine(AppContext.BaseDirectory, "ToDo Web APi.xml");
            setup.IncludeXmlComments(filePath);

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }

    /// <summary>
    /// AddDbContext
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IServiceCollection AuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IRequestUserProvider, RequestUserProvider>();
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ToDoDbContext>();

        services.AddScoped<IJwtService, JwtService>();

        JwtConfig jwtConfig = new();
        configuration.GetSection("JWT").Bind(jwtConfig);

        services.AddSingleton(jwtConfig);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                    };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("CanTest", policy =>
            {
                policy.RequireAuthenticatedUser();
                // policy.RequireClaim("CanTest");
                policy.Requirements.Add(new CanTestRequirment());
            });
        });

        return services;
    }
}