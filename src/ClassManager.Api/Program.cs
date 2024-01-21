using System.Text.Json.Serialization;
using Asp.Versioning.ApiExplorer;
using ClassManager.Api.Configurations;
using ClassManager.Api.Configurations.Swagger;
using ClassManager.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApi(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.ResolveIdentity(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ClassManagerDbContext>();
dbContext.Database.Migrate();

app.UseHsts();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
