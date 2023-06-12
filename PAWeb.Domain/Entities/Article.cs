using PAWebApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PAWeb.Domain.Entities
{
    public class Article
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public double Quantity { get; set; }
        public double Price { get; set; }

        public ICollection<TransactionArticle> TransactionArticles { get; set; }
    }
}
