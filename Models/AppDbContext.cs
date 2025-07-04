using Microsoft.EntityFrameworkCore;
using GA20201.Models;

namespace GA20201.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        
        public DbSet<Item> Items { get; set; }
    }
}
