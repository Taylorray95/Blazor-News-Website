using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace news_hogProj.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        public EmailSender(IConfiguration configuration)
        {
            _client = new SmtpClient
            {
                Host = configuration["Email:Smtp:Host"]!,
                Port = int.Parse(configuration["Email:Smtp:Port"]!),
                EnableSsl = bool.Parse(configuration["Email:Smtp:EnableSsl"]!),
                Credentials = new NetworkCredential(
                    configuration["Email:Smtp:Username"],
                    configuration["Email:Smtp:Password"]
                )
            };
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage("TaylorLeavelle@Fadasco.onmicrosoft.com", email, subject, htmlMessage) { IsBodyHtml = true };
            return _client.SendMailAsync(mailMessage);
        }
    }

}
