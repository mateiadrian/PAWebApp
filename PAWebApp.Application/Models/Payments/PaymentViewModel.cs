using PAWeb.Domain.Enums;
using PAWebApp.Application.Models.Customers;

namespace PAWebApp.Application.Models.Payments
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public PaymentType Type { get; set; }
        public string Currency { get; set; }
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
