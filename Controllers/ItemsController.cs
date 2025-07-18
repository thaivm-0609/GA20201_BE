using GA20201.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GA20201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ResponseApi response;
        public ItemsController(AppDbContext db)
        {
            _db = db;
            response = new ResponseApi();
        }

        [HttpGet] //url: /api/items
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var items = await _db.Items.ToListAsync();

                response.message = "Lấy dữ liệu thành công";
                response.success = true;
                response.data = items;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] //url: /api/items/{id}
        public async Task<IActionResult> GetDetail(Guid id)
        {
            try
            {
                //lay chi tiet ban ghi dua vao id
                var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);

                response.message = "Lấy dữ liệu thành công";
                response.success = true;
                response.data = item;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //xoa ban ghi
        [HttpDelete("{id}")] //url: /api/items/{id}
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                //B1: tim xem co ban ghi nao co ID tuong ung hay khong?
                var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
                //B2: kiem tra ko ton tai ban ghi -> return loi NotFound
                if (item == null)
                {
                    return NotFound();
                }
                //Neu co ton tai ban ghi -> thuc hien xoa
                _db.Items.Remove(item); //goi cau lenh xoa ban ghi item
                await _db.SaveChangesAsync(); //cap nhat vao trong db

                return Ok("Xoa thanh cong");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] //url: /api/items
        public async Task<IActionResult> CreateItem(Item item)
        {
            try
            {
                await _db.Items.AddAsync(item); //gui du lieu 
                await _db.SaveChangesAsync(); //luu thay doi vao trong database

                response.message = "Thêm mới thành công";
                response.success = true;
                response.data = item;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")] //url: /api/items/{id}
        public async Task<IActionResult> EditItem(Guid id, Item data)
        {
            try
            {
                //B1: kiem tra ban ghi trong db
                var oldItem = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
                if (oldItem == null)
                {
                    return NotFound();
                }
                //B2: gan du lieu tu data vao cho oldItem
                oldItem.Name = data.Name;
                oldItem.Price = data.Price;
                oldItem.Type = data.Type;
                oldItem.Description = data.Description;
                //B3: luu du lieu vao database
                await _db.SaveChangesAsync();

                response.message = "Chỉnh sửa thành công";
                response.success = true;
                response.data = oldItem;

                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0) //kiem tra neu khong co file upload len
                {
                    return BadRequest("Ban chua chon file");
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/uploads");
                Directory.CreateDirectory(path); //neu khong ton tai thu muc upload thi tao moi

                var uploadedFiles = new List<string>();

                foreach (var file in files)
                {
                    var filePath = Path.Combine(path, file.FileName);
                    using FileStream fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);
                    uploadedFiles.Add(file.FileName);
                }

                response.message = "Upload thành công";
                response.success = true;
                response.data = uploadedFiles;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
