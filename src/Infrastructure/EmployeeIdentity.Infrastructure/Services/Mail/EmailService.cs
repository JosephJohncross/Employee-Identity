using System.Net;
using System.Net.Mail;
using EmployeeIdentity.Application.Contracts.Infrastructure;
using EmployeeIdentity.Application.Models.Mail;
using Microsoft.Extensions.Options;

namespace EmployeeIdentity.Infrastructure.Services.Mail;

public class EmailService : IMail
{

    private readonly IOptions<SMTPParamterSettings> _smtpSettings;
    public EmailService(IOptions<SMTPParamterSettings> smtpSettings) => _smtpSettings = smtpSettings;

    public async Task SendEmailAsync(SendEmailParameters emailParamters)
    {
        var message = new MailMessage(emailParamters.From, emailParamters.To, emailParamters.Subject, emailParamters.Body);

        using var emailclient = new SmtpClient(_smtpSettings.Value.Host, _smtpSettings.Value.Port);
        emailclient.Credentials = new NetworkCredential(_smtpSettings.Value.UserName, _smtpSettings.Value.Password);

        await emailclient.SendMailAsync(message);
    }
}