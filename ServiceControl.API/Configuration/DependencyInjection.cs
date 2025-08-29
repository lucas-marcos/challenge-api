using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.Services;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Services;

namespace ServiceControl.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        // Services
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<IServiceBClient, ServiceBClient>();
        services.AddScoped<INotificationService, NotificationService>();
        
        // Services
        services.AddScoped<IProcessOrderService, ProcessOrderService>();
        
        // Logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.SetMinimumLevel(LogLevel.Information);
        });
        
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Cache e HTTP Client
        services.AddMemoryCache();
        services.AddHttpClient();
        
        return services;
    }
}
