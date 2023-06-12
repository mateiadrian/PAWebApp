using PAWebApp.Application.Models.Articles;
using System.Threading;

namespace PAWebApp.Application.Services.ArticleService
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<ArticleViewModel> GetByIdAsync(int articleId, CancellationToken cancellationToken);
        Task<ArticleViewModel> AddAsync(AddArticleRequestModel articleModel, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ArticleViewModel> UpdateAsync(AddArticleRequestModel articleModel, CancellationToken cancellationToken);
        Task<IEnumerable<ArticleViewModel>> GetByIds(List<int> articleIds, CancellationToken cancellationToken);
    }
}
