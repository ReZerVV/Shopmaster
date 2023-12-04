using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Shopmaster.Application.Commands.Auth;

namespace Shopmaster.Infrastructure.Services.Auth;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public void Send(string to, string subject, string body)
    {
        var smtp = new SmtpClient
        {
            Host = _emailSettings.Host,
            Port = _emailSettings.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                _emailSettings.AccountLogin,
                _emailSettings.AccountPassword
            )
        };

        using var message = new MailMessage(_emailSettings.AccountLogin, to: to);
        message.Subject = subject;
        message.Body = body;

        smtp.Send(message);
    }
}
