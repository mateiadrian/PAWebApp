using PAWebApp.Domain.Entities;

namespace PAWeb.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public double TotalArticles { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public ICollection<TransactionArticle> TransactionArticles { get; set; }
    }
}
