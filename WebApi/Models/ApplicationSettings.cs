namespace WebApi.Models
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; } = null!;
        public string notActivated { get; set; }
        public string failedlogin { get; set; }
    }
}