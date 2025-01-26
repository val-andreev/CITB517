namespace RefleciaProject.Services
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Configuration;
    using MimeKit;
    using System.Threading.Tasks;

    namespace RefleciaProject.Services
    {
        public class EmailService
        {
            private readonly IConfiguration _configuration;

            public EmailService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task SendEmailAsync(string to, string subject, string body)
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Reflecia Project", _configuration["EmailSettings:SenderEmail"]));
                email.To.Add(new MailboxAddress("", to));
                email.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = body };
                email.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    //ako e null hardcode na 587
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}