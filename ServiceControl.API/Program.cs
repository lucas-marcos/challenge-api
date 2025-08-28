using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.UseCases;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Services;

namespace ServiceControl.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<ServiceControlDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddMemoryCache();
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddScoped<IServiceBClient, ServiceBClient>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IProcessOrderUseCase, ProcessOrderUseCase>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
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

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ServiceControlDbContext>();
                context.Database.Migrate();
            }

            app.Run();
        }
    }
}
