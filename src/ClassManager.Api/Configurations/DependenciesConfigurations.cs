using Asp.Versioning;
using ClassManager.Api.Configurations.Swagger;
using ClassManager.Api.Identity;
using ClassManager.Business.Authentication;
using ClassManager.Business.Entities.Validators;
using ClassManager.Business.Notifications;
using ClassManager.Business.Profiles;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Abstract;
using ClassManager.Business.Services.Concrete;
using ClassManager.Data.Authentication;
using ClassManager.Data.Context;
using ClassManager.Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClassManager.Api.Configurations;

public static class DependenciesConfigurations
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUser, AspNetUser>();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        ConfigureApiVersioning(services);
        ResolveAutoMapper(services);
        ResolveValidators(services);
        ResolveServices(services);
        ResolveRepositories(services);
        ConfigureDatabase(services, configuration);
        return services;
    }

    private static void ConfigureApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("api-version"),
                new MediaTypeApiVersionReader("api-version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";

            options.SubstituteApiVersionInUrl = true;
        });
    }

    private static void ResolveAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UsuarioProfile).Assembly);
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClassManagerDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("Default")));
    }

    private static void ResolveValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
    }

    private static void ResolveRepositories(IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        
    }

    private static void ResolveServices(IServiceCollection services)
    {
        services
            .AddScoped<IUsuarioService, UsuarioService>()
            .AddScoped<INotificationServce, NotificationService>()
            .AddScoped<IIdentityService, IdentityService>();
    }
}
