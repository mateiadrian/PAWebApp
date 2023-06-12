using PAWeb.Domain.Enums;

namespace PAWebApp.Application.Models.Payments
{
    public class AddPaymentRequestModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public PaymentType Type { get; set; }
        public string Currency { get; set; }
        public int CustomerId { get; set; }
    }
}
