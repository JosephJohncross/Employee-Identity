using MailKit.Net.Smtp;
using EmployeeIdentity.Application.Contracts.Infrastructure;
using EmployeeIdentity.Application.Models.Mail;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace EmployeeIdentity.Infrastructure.Services.Mail;

public class EmailService : IMail
{
    private readonly SMTPParamterSettings _smtpSettings;
    public EmailService(IOptions<SMTPParamterSettings> smtpSettings) => _smtpSettings = smtpSettings.Value;

    public async Task SendEmailAsync(SendEmailParameters emailParamters)
    {
        var email = new MimeMessage(){
            Subject = emailParamters.Subject,
            To = {MailboxAddress.Parse(emailParamters.To)},
            Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                Text = emailParamters.Body
            },
            From = { MailboxAddress.Parse(emailParamters.From)}
        };

        using (var smtp = new SmtpClient()){
            smtp.Connect(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpSettings.UserName, _smtpSettings.Password);

            var response = smtp.Send(email);
            smtp.Disconnect(true);
        }
        
    }
}