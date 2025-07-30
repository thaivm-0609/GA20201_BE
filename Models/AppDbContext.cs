using Microsoft.EntityFrameworkCore;
using GA20201.Models;

namespace GA20201.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        
        // DbSet<TenModel> TenTable { get; set; }
        public DbSet<Item> Items { get; set; } //khai bao table

        public DbSet<Account> Accounts { get; set; } //khai bao bang account
    }
}
