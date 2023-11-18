
using System.Reflection;
using ClassManager.Business.Entities.Validators;
using ClassManager.Business.Notifications;
using ClassManager.Business.Profiles;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Concretos;
using ClassManager.Business.Services.Interfaces;
using ClassManager.Data.Authentication;
using ClassManager.Data.Context;
using ClassManager.Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Api.Configurations;

public static class DependenciesConfigurations
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        ResolveAutoMapper(services);
        ResolveValidators(services);
        ResolveServices(services);
        ResolveRepositories(services);
        ConfigureSwagger(services);
        ConfigureDatabase(services, configuration);
        return services;
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
            .AddScoped<ILoginService, LoginService>();
    }

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => 
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}