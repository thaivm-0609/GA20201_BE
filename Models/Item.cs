using System.ComponentModel.DataAnnotations;

namespace GA20201.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
