namespace PaymentService.API.Models
{
    public class PaymentDto
    {
        public string CardNumber { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public string Expiration { get; set; } = null!;
        public int CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
