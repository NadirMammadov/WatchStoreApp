﻿namespace PaymentService.API.Models
{
    public class PaymentDto
    {
        public string CardNumber { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public string Expiration { get; set; } = null!;
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderDto Order { get; set; }
    }
}
