namespace WatchStoreApp.UI.Models
{
    public class ClientSettings
    {
        public Client? WebClient { get; set; }
        public Client? WebClientForUser { get; set; }
    }
    public class Client
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }
}
