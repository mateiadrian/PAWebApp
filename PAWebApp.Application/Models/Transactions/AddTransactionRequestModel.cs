using PAWebApp.Application.Models.Payments;

namespace PAWebApp.Application.Models.Transactions
{
    public class AddTransactionRequestModel
    {
        public AddTransactionRequestModel()
        {
            Articles = new List<TransactionArticleModel>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }

        public IList<TransactionArticleModel> Articles { get; set; }
        public AddPaymentRequestModel Payment { get; set; }
    }
}
