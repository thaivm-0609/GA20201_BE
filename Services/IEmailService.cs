using GA20201.Models.Email;
using Microsoft.AspNetCore.Identity.Data;

namespace GA20201.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(EmailRequest emailRequest);
    }
}
