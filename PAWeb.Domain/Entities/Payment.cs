using PAWeb.Domain.Enums;

namespace PAWeb.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public PaymentType Type { get; set; }
        public string Currency { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}