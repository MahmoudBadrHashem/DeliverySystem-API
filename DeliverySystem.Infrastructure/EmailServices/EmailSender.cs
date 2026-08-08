using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace DeliverySystem.Infrastructure.EmailServices;

public class EmailSender(IConfiguration _configuration) : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_configuration["EmailInfo:UserName"], _configuration["EmailInfo:Password"])
        };

        return client.SendMailAsync(
        new MailMessage(from: _configuration["EmailInfo:UserName"]!,
                        to: email,
                        subject,
                        htmlMessage
                        )
        {
            IsBodyHtml = true
        });
    }

}