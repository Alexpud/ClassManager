using System.Text;
using ClassManager.Api.Identity;
using ClassManager.Business.Entities;
using ClassManager.Data.Context;
using ClassManager.Data.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ClassManager.Api.Configurations;

public static class AuthenticationConfiguration
{
    public static void ResolveIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureIdentity(services);
        ConfigureAuthentication(services, configuration);
        ConfigureAuthorization(services);
    }

    private static void ConfigureIdentity(IServiceCollection services)
    {
        services
            .AddIdentity<Usuario, IdentityRole<Guid>>()
            .AddRoles<IdentityRole<Guid>>()
            .AddErrorDescriber<CustomIdentityErrors>()
            .AddEntityFrameworkStores<ClassManagerDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AuthenticationSettings>().Bind(configuration.GetSection("Authentication"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Secret"])),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Coordenador", policy => policy.RequireRole("Coordenador"));
            options.AddPolicy("Discentes", policy => policy.RequireRole("Coordenador", "Professor"));
            options.AddPolicy("ProfessoresEAlunos", policy => policy.RequireRole("Usuario", "Professor"));
        });
    }
}