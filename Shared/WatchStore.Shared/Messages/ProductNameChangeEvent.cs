namespace WatchStore.Shared.Messages
{
    public class ProductNameChangeEvent
    {
        public string ProductId { get; set; } = null!;
        public string UpdatedName { get; set; } = null!;

    }
}
