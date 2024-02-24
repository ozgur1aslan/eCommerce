using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your Name", _configuration["EmailSettings:SenderEmail"]));
        message.To.Add(new MailboxAddress("Recipient Name", to));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            client.Connect(_configuration["EmailSettings:SmtpServer"],
                           int.Parse(_configuration["EmailSettings:SmtpPort"]), false);
            client.Authenticate(_configuration["EmailSettings:SenderEmail"],
                                _configuration["EmailSettings:SenderPassword"]);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
