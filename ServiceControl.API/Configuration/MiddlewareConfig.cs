using Microsoft.AspNetCore.Builder;

namespace ServiceControl.API.Configuration;

public static class MiddlewareConfig
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        // Middleware personalizado pode ser adicionado aqui
        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        return app;
    }
}
