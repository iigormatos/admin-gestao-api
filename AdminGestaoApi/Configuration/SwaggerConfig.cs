using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;

namespace AdminGestaoApi.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Gestão Admin",
                    Description = "API de gestão.",
                    Contact = new OpenApiContact() { Name = "AdminGestaoApi", Email = "igormatos.andrade@gmail.com" }
                });

                c.AddSecurityDefinition("token",
                                        new OpenApiSecurityScheme
                                        {
                                            Type = SecuritySchemeType.Http,
                                            BearerFormat = "JWT",
                                            Scheme = "Bearer",
                                            In = ParameterLocation.Header,
                                            Name = HeaderNames.Authorization
                                        });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
