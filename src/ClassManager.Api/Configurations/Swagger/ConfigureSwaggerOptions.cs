
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClassManager.Api.Configurations.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var version in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(version.GroupName, new OpenApiInfo
            {
                Version = version.ApiVersion.ToString(),
                Title = $"Versão da API{(version.IsDeprecated ? " - versão deprecada" : "")}"
            });
        }
    }
}
