using EmployeeIdentity.Application.Models.Mail;

namespace EmployeeIdentity.Application.Contracts.Infrastructure;

public interface IMail
{
    public Task SendEmailAsync(SendEmailParameters emailParamters);
}