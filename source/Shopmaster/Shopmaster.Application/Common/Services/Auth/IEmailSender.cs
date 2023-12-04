namespace Shopmaster.Application.Commands.Auth;

public interface IEmailSender
{
    void Send(string to, string subject, string body);
}