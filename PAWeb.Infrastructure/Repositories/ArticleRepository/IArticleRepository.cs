using PAWeb.Domain.Entities;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.ArticleRepository
{
    public interface IArticleRepository : IRepository<Article>
    {
        void SeedData();
    }
}
