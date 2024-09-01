namespace AuthenticationProvider.Models.Main
{
    public class AppSettings
    {
        public EmailConfig EmailConfig { get; set; }
    }


    public class EmailConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailFrom { get; set; }
    }
}
