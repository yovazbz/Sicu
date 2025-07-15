using Microsoft.OpenApi.Models;

namespace Sicu.Modules.Swagger;

/// <summary>
/// SwaggerExtensions
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// IServiceCollection : AddSwagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Created the Swagger document
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "Version 1.0.x",
                Title = "Swagger UI - API ",
                Description = "<h2>Thanks for sharing.",
                TermsOfService = new Uri("https://anam.gob.mx/"),
            });

            // second form to give Authorization without whrite the world Bearer
            var securityScheme = new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT" // Optional
            };

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    new string[] {}
                }
            };
            c.AddSecurityDefinition("bearerAuth", securityScheme);
            c.AddSecurityRequirement(securityRequirement);
        });

        return services;
    } // end
}