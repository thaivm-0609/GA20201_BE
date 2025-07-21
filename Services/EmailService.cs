using GA20201.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GA20201.Services
{
    //ke thua lai interface IEmailService
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> es)
        {
            _emailSettings = es.Value;
        }

        //khai bao thong tin email
        public async Task SendMailAsync(EmailRequest emailRequest)
        {
            //B1: khoi tao 1 mau email
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.senderName, _emailSettings.senderEmail)); //thong tin ng gui email
            email.To.Add(MailboxAddress.Parse(emailRequest.To)); //thong tin ng nhan email
            email.Subject = emailRequest.Subject;
            //Truong hop noi dung email chi toan van ban
            email.Body = new TextPart("plain") { Text = emailRequest.Body };

            //B2: thuc hien gui email thong qua SmtpClient:
            var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _emailSettings.smtpServer, 
                _emailSettings.smtpPort, 
                SecureSocketOptions.StartTls
            ); //tao ket noi den sv gui email
            await smtp.AuthenticateAsync(_emailSettings.username, _emailSettings.password); //thong tin dang nhap vao dich vu gui mail
            await smtp.SendAsync(email); //gui email da tao phia tren
            await smtp.DisconnectAsync(true); //ngat ket noi SmtpClient sau khi gui xong
        }
    }
}
