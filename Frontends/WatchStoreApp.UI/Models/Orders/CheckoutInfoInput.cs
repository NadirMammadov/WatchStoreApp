using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "Şəhər")]
        public string Province { get; set; } = null!;
        [Display(Name = "Rayon")]
        public string District { get; set; } = null!;
        [Display(Name = "Küçə")]
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        [Display(Name = "Kart nömrəsi")]
        public string CardNumber { get; set; } = null!;
        [Display(Name = "Kart üzərindəki ad")]
        public string CardName { get; set; } = null!;
        [Display(Name = "Kartin son istifadə tarixi")]
        public string Expiration { get; set; } = null!;
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
