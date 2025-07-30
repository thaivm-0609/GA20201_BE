using GA20201.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks.Sources;

namespace GA20201.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public LoginController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        //ham dang nhap
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]string username, [FromForm]string password)
        {
            try
            {
                var existAcc = await _db.Accounts.FirstOrDefaultAsync(acc => 
                    acc.Username == username 
                    && acc.Password == password
                );
                if (existAcc != null) //neu ton tai acc o trong db thi tao token
                {
                    var token = CreateToken(username); //goi ham CreateToken de tao token

                    return Ok(token);
                } else
                {
                    return BadRequest("Thông tin tài khoản không đúng");
                }

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //ham tao token tu thong tin username
        private string CreateToken(string username)
        {
            var jwt = _config.GetSection("Jwt"); //lay thong tin jwt
            var key = Encoding.UTF8.GetBytes(jwt["Key"]); //lay key tu jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            { //khai bao thong tin de tao ra token
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1), //token het han sau 1 phut
                Issuer = jwt["Issuer"],
                Audience = jwt["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            //khoi tao token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
