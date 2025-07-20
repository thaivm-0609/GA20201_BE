using GA20201.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorLight;

namespace GA20201.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly RazorLightEngine _razor;
        public EmailService(IOptions<EmailSettings> es)
        {
            _emailSettings = es.Value;

            //thêm RazorLightEngine
            _razor = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Views"))
                .UseMemoryCachingProvider().Build();
        }

        public async Task SendMailAsync(EmailRequest emailRequest)
        {
            //khoi tao 1 email moi
            var email = new MimeMessage();
            //khai bao thong tin emailRequest
            email.From.Add(new MailboxAddress(_emailSettings.senderName, _emailSettings.senderEmail));
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            email.Subject = emailRequest.Subject;
            
            //neu gui template mail
            if (emailRequest.IsHtml)
            {
                string html = await _razor.CompileRenderAsync("EmailTemplate.cshtml", new EmailTemplate
                {
                    name = "Vương Minh Thái",
                    message = "Đây là message"
                });
                email.Body = new TextPart("html") { Text = html };
            } else
            {
                email.Body = new TextPart("plain") { Text = emailRequest.Body };
            }

            //gui mail
            var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.smtpServer, _emailSettings.smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.username, _emailSettings.password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
