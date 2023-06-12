using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PAWeb.Domain.Entities;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Infrastructure.Repositories.ArticleRepository;
using System.Threading;

namespace PAWebApp.Application.Services.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddArticleRequestModel> _validator;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper, IValidator<AddArticleRequestModel> validator)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ArticleViewModel> GetByIdAsync(int articleId, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(articleId, cancellationToken);

            return article == null
                ? throw new EntityNotFoundException($"Entity not found with the id {articleId}")
                : _mapper.Map<Article, ArticleViewModel>(article);
            ;
        }

        public async Task<ArticleViewModel> AddAsync(AddArticleRequestModel articleModel, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(articleModel, cancellationToken);
            if (!validationResult.IsValid)
                throw new ModelValidationException(validationResult.Errors);

            var articleDbModel = _mapper.Map<AddArticleRequestModel, Article>(articleModel);

            var article = await _articleRepository.AddAsync(articleDbModel, cancellationToken);

            return _mapper.Map<Article, ArticleViewModel>(article);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(id, cancellationToken);

            if (article == null)
                throw new EntityNotFoundException($"Entity not found with the id {id}");

            await _articleRepository.DeleteAsync(article, cancellationToken);
        }

        public async Task<IEnumerable<ArticleViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.GetAllAsync(cancellationToken);

            return articles.Select(_mapper.Map<Article, ArticleViewModel>).ToList();
        }

        public async Task<ArticleViewModel> UpdateAsync(AddArticleRequestModel articleModel, CancellationToken cancellationToken)
        {
            var articleDbModel = _mapper.Map<AddArticleRequestModel, Article>(articleModel);

            var updatedArticleModel = await _articleRepository.UpdateAsync(articleDbModel, cancellationToken);

            return _mapper.Map<Article, ArticleViewModel>(updatedArticleModel);
        }

        public async Task<IEnumerable<ArticleViewModel>> GetByIds(List<int> ids, CancellationToken cancellationToken)
        {
            var query = _articleRepository.GetQueriable()
                .Where(c => ids.Contains(c.Id))
                .AsQueryable();

            var articles = await query.ToListAsync(cancellationToken);

            return articles.Select(_mapper.Map<Article, ArticleViewModel>).ToList();
        }
    }
}
 