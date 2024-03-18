
using System.Reflection;
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Infrastructure.Contracts;
using EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeIdentity.Infrastructure;

public static class InfrastructureServiceRegisteration
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
    {        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IAuthService<>), typeof(AuthRepository<>));
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        return services;
    }
}