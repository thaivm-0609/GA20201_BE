using GA20201.Models.Email;

namespace GA20201.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(EmailRequest emailRequest);
    }
}
