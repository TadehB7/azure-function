using System.Net;
using System.Net.Mail;

namespace WeatherEmailFunction.Services;

public class EmailService
{
    public async Task SendAsync(string body)
    {
        /*        var from = Environment.GetEnvironmentVariable("Email_User");
                var password = Environment.GetEnvironmentVariable("Email_Password");
                var to = Environment.GetEnvironmentVariable("Recipient_Email");*/

        var from = "parscraneco@gmail.com";
        var password = "mjbt pexr edgf qvzp";
        var to = "tadehb83@gmail.com";

        if (string.IsNullOrWhiteSpace(from))
            throw new InvalidOperationException("Missing EmailUser setting.");

        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("Missing EmailPassword setting.");

        if (string.IsNullOrWhiteSpace(to))
            throw new InvalidOperationException("Missing RecipientEmail setting.");

        using var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(from, password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false
        };

        using var message = new MailMessage(from, to)
        {
            Subject = "Weather Report",
            Body = body
        };

        await client.SendMailAsync(message);
    }
}
