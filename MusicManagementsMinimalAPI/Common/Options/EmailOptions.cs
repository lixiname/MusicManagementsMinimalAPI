namespace MusicManagementsMinimalAPI.Common.Options
{
    public class EmailOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string SendEmail { get; set; }

        public string ReceiveEmail { get; set; }

        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DefaultFromName { get; set; }
    }
}
