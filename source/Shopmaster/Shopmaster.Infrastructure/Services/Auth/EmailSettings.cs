namespace Shopmaster.Infrastructure.Services.Auth;

public class EmailSettings
{
    public const string SectionName = "SmtpSettings";
    public string Host { get; set; }
    public int Port { get; set; }
    public string AccountLogin { get; set; }
    public string AccountPassword { get; set; }
}