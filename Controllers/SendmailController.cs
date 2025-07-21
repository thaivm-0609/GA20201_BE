using GA20201.Models.Email;
using GA20201.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GA20201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public SendmailController(IEmailService es)
        {
            _emailService = es;
        }

        [HttpPost]
        public async Task<IActionResult> Sendmail([FromBody] EmailRequest emailRequest)
        {
            try
            {
                await _emailService.SendMailAsync(emailRequest);

                return Ok("Gui mail thanh cong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
