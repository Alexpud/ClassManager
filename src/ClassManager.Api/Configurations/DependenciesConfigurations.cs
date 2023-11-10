
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Concretos;
using ClassManager.Business.Services.Interfaces;
using ClassManager.Data.Repositories;

namespace ClassManager.Api.Configurations;

public static class DependenciesConfigurations
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection serviceCollection)
    {
        ResolveServices(serviceCollection);
        ResolveRepositories(serviceCollection);
        return serviceCollection;
    }

    private static void ResolveRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }

    private static void ResolveServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
    }
}