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

        public ItemsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet] //url: /api/items
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var items = await _db.Items.ToListAsync();
                return Ok(items);
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

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] //url: /api/items
        public async Task<IActionResult> CreateItem(Item data)
        {
            try
            {
                await _db.Items.AddAsync(data); //lay du lieu moi 
                await _db.SaveChangesAsync(); //luu thay doi vao database

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")] //url: /api/items/{id}
        public async Task<IActionResult> UpdateItem(Guid id, Item data)
        {
            try
            {
                var existingItem = await _db.Items.FindAsync(id);
                if (existingItem == null)
                {
                    return NotFound("Không tìm thấy item");
                }

                //gan lai gia tri moi cho ban ghi
                existingItem.Name = data.Name;
                existingItem.Price = data.Price;
                existingItem.Type = data.Type;
                existingItem.Description = data.Description;
                await _db.SaveChangesAsync(); //luu thay doi vao database

                return Ok(existingItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] //url: /api/items/{id}
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                //tim ban ghi co id duoc truyen vao url
                var item = await _db.Items.FindAsync(id);
                if (item == null) //neu khong tim thay ban ghi thi bao loi
                {
                    return NotFound();
                }
                _db.Items.Remove(item); //xoa ban ghi item da tim thay
                await _db.SaveChangesAsync();

                return Ok("Xoa thanh cong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
