using ServiceControl.API.Configuration;

namespace ServiceControl.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurações de serviços
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddCorsPolicy();
            builder.Services.AddDatabaseServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            var app = builder.Build();

            // Configurações de middleware
            app.UseSwaggerServices(app.Environment);
            app.UseCorsPolicy();
            app.UseCustomMiddleware();
            app.MapControllers();
            app.UseDatabaseServices();

            app.Run();
        }
    }
}
