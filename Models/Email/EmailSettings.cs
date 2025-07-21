using Microsoft.Identity.Client;

namespace GA20201.Models.Email
{
    public class EmailSettings //khai bao thong tin cau hinh
    {
        public string? smtpServer { get; set; } //dung loai server nao de gui mail
        public int smtpPort { get; set; } //so port su dung: thuong la 587
        public string? senderName { get; set; } //ten nguoi gui mail
        public string? senderEmail { get; set; } //email nguoi gui
        public string? username { get; set; } //ten tk gui mail
        public string? password { get; set; } //mat khau tk gui mail
    }
}
