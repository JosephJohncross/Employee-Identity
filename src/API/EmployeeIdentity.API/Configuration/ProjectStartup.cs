using Asp.Versioning.ApiExplorer;
using EmployeeIdentity.Application;
using EmployeeIdentity.Infrastructure;
using EmployeeIdentity.Persistence;
using Serilog;

namespace EmployeeIdentity.API;

public static class ProjectStartup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config, WebApplicationBuilder builder)
    {
        services.AddEndpointsApiExplorer();

        services.AddApplicationServices();
        services.AddInfrastructureService();
        services.AddPersistenceService(config);
       
        services.AddHealthChecks()
                .AddNpgSql(config.GetConnectionString("Default") ?? "");                
        
        
        services.AddLogging();

        builder.Host.UseSerilog((context, loggerConfig) => {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
        });

        return services;
    }
}