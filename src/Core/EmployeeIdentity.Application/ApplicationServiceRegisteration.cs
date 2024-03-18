using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace EmployeeIdentity.Application;

public static class ApplicationServiceRegisteration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));        
        
        return services;
    }
}