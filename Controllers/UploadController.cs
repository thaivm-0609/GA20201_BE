using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GA20201.Controllers
{
    [Route("api/[controller]")] // /api/upload
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        //upload 1 file: UploadFile(<IFormFile> file)
        //upload nhieu file: UploadFile(List<IFormFile> files)
        //[FromForm]: khai bao du lieu se duoc gui qua form
        public async Task<IActionResult> UploadFile([FromForm] List<IFormFile> files)
        {
            try
            {
                //B1: kiem tra xem ng dung co day file len khong?
                if (files == null || files.Count == 0)
                {
                    return BadRequest("Ban chua chon file");
                }
                //B2: thuc hien upload file
                //B2.1: khai bao vi tri luu tru file: path: la duong dan den thu muc chua file
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(path); //tao thu muc neu nhu chua co

                //B2.2: upload file len server
                foreach (var file in files) 
                {
                    //duong dan den file = path + tenFile;
                    var filePath = Path.Combine(path, file.FileName); 
                    //su dung FileStream de tao luong upload file
                    using FileStream fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);
                }

                return Ok("Upload thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
