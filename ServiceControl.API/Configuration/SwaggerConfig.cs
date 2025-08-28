using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ServiceControl.API.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ServiceControl API",
                Version = "v1",
                Description = "API para gerenciamento de pedidos com integração de clima",
                Contact = new OpenApiContact
                {
                    Name = "E-mail - Lucas Marcos ",
                    Email = "lucas_lmms@hotmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "LinkedIn - Lucas Marcos",
                    Url = new Uri("https://www.linkedin.com/in/lucas-marcos-a813a81b3/")
                }
            });

            c.CustomSchemaIds(type => type.Name);
        });
        
        return services;
    }

    public static IApplicationBuilder UseSwaggerServices(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceControl API v1");
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "ServiceControl API Documentation";
                c.DefaultModelsExpandDepth(2);
                c.DefaultModelExpandDepth(2);
            });
        }
        
        return app;
    }
}
