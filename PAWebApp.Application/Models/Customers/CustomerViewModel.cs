using PAWebApp.Application.Models.Transactions;

namespace PAWebApp.Application.Models.Customers
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public IList<AddTransactionRequestModel> Transactions { get; set; }
    }
}
