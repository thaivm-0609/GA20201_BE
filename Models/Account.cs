namespace GA20201.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int Active { get; set; }
    }
}
