using GA20201.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace GA20201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly ResponseApi res;

        public LoginController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
            res = new ResponseApi();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]string username, [FromForm]string password)
        {
            try
            {
                var existAcc = await _db.Accounts.FirstOrDefaultAsync(acc => acc.Username == username && acc.Password == password);
                if (existAcc != null)
                {
                    var token = CreateToken(username);
                    res.Message = "Đăng nhập thành công";
                    res.Success = true;
                    res.Data = token;

                    return Ok(res);
                } else
                {
                    res.Message = "Đăng nhập thất bại";
                    res.Success = false;
                    res.Data = "Tài khoản hoặc mật khẩu không đúng";
                    
                    return BadRequest(res);
                }
            } catch (Exception ex) {
                res.Message = "Đăng nhập thất bại";
                res.Success = false;
                res.Data = ex.Message;

                return BadRequest(res);
            }
        }

        //phuong thuc tao token
        private string CreateToken(string username)
        {
            var jwt = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor //thong tin token
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1), //thoi gian ton tai
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwt["Issuer"],
                Audience = jwt["Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
