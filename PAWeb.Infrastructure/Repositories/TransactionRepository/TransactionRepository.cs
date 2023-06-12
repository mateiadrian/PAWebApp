using Microsoft.EntityFrameworkCore;
using PAWeb.Domain.Entities;
using PAWeb.Infrastructure;
using PAWebApp.Domain.Entities;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.TransactionRepository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext dbContext) : base(dbContext)
        {
            
        }

        public async Task AddTransactionArticle(int transactionId, int articleId, CancellationToken cancellationToken)
        {
            var transactionArticle = new TransactionArticle { ArticleId = articleId, TransactionId = articleId };

            _dbContext.TransactionArticles.Add(transactionArticle);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
