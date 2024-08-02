using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FIAP.TECH.CORE.APPLICATION.Settings.Swagger
{
    public static class SwaggerConfig
    {
        private const string SpecifOrigensCors = "_specifOrigensCors";

        public static void AddSwaggerConfgi(this IServiceCollection services)
        {
            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira seu token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
            #endregion
        }
    }
}
