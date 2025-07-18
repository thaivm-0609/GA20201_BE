namespace GA20201.Controllers
{
    public class ResponseApi
    {
        public string message { get; set; } = string.Empty;

        public bool success { get; set; }

        public object? data { get; set; }
    }
}
