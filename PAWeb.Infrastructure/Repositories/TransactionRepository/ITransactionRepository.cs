using PAWeb.Domain.Entities;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.TransactionRepository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public Task AddTransactionArticle(int transactionId, int articleId, CancellationToken cancellationToken);
    }
}
