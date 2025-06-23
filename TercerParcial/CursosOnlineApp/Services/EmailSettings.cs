namespace CursosOnlineApp.Models
{
    public class EmailSettings
    {
        public required string ApiKey { get; set; }
        public required string FromEmail { get; set; }
        public required string FromName { get; set; }
    }
}