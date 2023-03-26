﻿namespace WatchStoreApp.UI.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUrl { get; set; } = null!;
        public string GatewayBaseUrl { get; set; } = null!;
        public string PhotoStockUrl { get; set; } = null!;
        public ServiceApi Catalog { get; set; } = null!;
        public ServiceApi PhotoStock { get; set; } = null!;
    }
    public class ServiceApi
    {
        public string Path { get; set; } = null!;
    }
}
