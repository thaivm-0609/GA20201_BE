using GA20201.Models.Email;
using GA20201.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GA20201.Controllers
{
    [Route("api/sendmail")]
    [ApiController]
    public class SendmailController : ControllerBase
    {
        protected readonly IEmailService _es;

        public SendmailController(IEmailService es)
        {
            _es = es;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] EmailRequest emailRequest)
        {
            try
            {
                await _es.SendMailAsync(emailRequest);

                return Ok("Gửi mail thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
