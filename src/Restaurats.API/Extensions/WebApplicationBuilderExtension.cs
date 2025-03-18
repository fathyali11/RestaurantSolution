using Microsoft.OpenApi.Models;
using Restaurats.API.Middlewares;
using Serilog;

namespace Restaurats.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id="bearer"
                }
              },
              []
        }
    });
        });

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

    }
}
