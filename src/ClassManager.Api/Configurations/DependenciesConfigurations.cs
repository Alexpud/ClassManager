
using System.Reflection;
using System.Runtime.CompilerServices;
using ClassManager.Business.Entities.Validators;
using ClassManager.Business.Notifications;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Concretos;
using ClassManager.Business.Services.Interfaces;
using ClassManager.Data.Repositories;
using FluentValidation;

namespace ClassManager.Api.Configurations;

public static class DependenciesConfigurations
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        ResolveValidators(services);
        ResolveServices(services);
        ResolveRepositories(services);
        ConfigureSwagger(services);
        return services;
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
            .AddScoped<INotificationServce, NotificationService>();
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