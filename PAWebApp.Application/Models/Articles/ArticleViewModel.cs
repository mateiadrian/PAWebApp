using PAWebApp.Application.Models.Transactions;

namespace PAWebApp.Application.Models.Articles
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            Transactions = new List<AddTransactionRequestModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Inventory { get; set; }
        public double Price { get; set; }

        public IList<AddTransactionRequestModel> Transactions { get; set; }
    }
}
