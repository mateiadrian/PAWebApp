using PAWeb.Domain.Entities;

namespace PAWebApp.Domain.Entities
{
    public class TransactionArticle
    {
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
