using Microsoft.EntityFrameworkCore.Storage;

namespace PAWebApp.Infrastructure.Repositories.BaseRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        IQueryable<TEntity> GetQueriable();
        Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken);
    }
}