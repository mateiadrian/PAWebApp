using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PAWeb.Infrastructure;

namespace PAWebApp.Infrastructure.Repositories.BaseRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DatabaseContext _dbContext;
        private readonly DbSet<TEntity> _entities;

        public Repository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var addedEntity = await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return addedEntity.Entity;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                .FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual IQueryable<TEntity> GetQueriable()
        {
            return _entities.AsQueryable();
        }

        public virtual async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
