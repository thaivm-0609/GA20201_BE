namespace GA20201.Models.Email
{
    //chua hong tin nguoi nhan, tieu de mail, noi dung mail
    public class EmailRequest
    {
        public string? To { get; set; } //thong tin nguoi nhan

        public string? Subject { get; set; } //tieu de cua email

        public string? Body { get; set; } //noi dung email
        public bool isHtml {  get; set; } //khai bao dinh dang email la html hay la text thuong
    }
}
