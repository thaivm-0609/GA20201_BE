using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GA20201.Models.Email
{
    public class EmailSettings
    {
        public string smtpServer {  get; set; }
        public int smtpPort { get; set; }
        public string senderName { get; set; }
        public string senderEmail { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
