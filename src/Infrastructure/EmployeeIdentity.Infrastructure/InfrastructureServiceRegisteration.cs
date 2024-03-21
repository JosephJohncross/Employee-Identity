
using System.Reflection;
using EmployeeIdentity.Application.Contracts.Identity;
using EmployeeIdentity.Application.Contracts.Infrastructure;
using EmployeeIdentity.Application.Models.Mail;
using EmployeeIdentity.Infrastructure.Contracts;
using EmployeeIdentity.Infrastructure.Services.Mail;
using EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeIdentity.Infrastructure;

public static class InfrastructureServiceRegisteration
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IAuthService<>), typeof(AuthRepository<>));
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddScoped<IMail, EmailService>();
        services.AddHttpContextAccessor();
        services.Configure<SMTPParamterSettings>(config.GetSection("EmailSettings"));
        return services;
    }
}