namespace DiscountService.API.Models
{
    [Dapper.Contrib.Extensions.Table("discount")]
    public class Discount
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int Rate { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
    }
}
