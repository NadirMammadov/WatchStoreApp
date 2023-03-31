using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "Il")]
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string? Line { get; set; }
        public string CardNumber { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public string Expiration { get; set; } = null!;
        public int CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
