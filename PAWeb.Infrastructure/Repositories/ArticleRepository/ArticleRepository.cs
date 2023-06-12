using PAWeb.Domain.Entities;
using PAWeb.Infrastructure;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.ArticleRepository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public void SeedData()
        {
            if (!_dbContext.Articles.Any())
            {
                var articles = new List<Article>
                {
                    new Article { Id = 1, Name = "Cola", Price = 10, Quantity = 1000 },
                    new Article { Id = 2, Name = "Fanta", Price = 10, Quantity = 2000 },
                    new Article { Id = 3, Name = "Sprite", Price = 10, Quantity = 2000 }
                };

                _dbContext.Articles.AddRange(articles);
                _dbContext.SaveChanges();
            }
        }
    }
}
