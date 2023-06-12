using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Services.ArticleService;

namespace PAWebApp.API.Controllers
{
    [ApiController]
    [Route("api/article")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IValidator<AddArticleRequestModel> _validator;

        public ArticleController(IArticleService articleService, IValidator<AddArticleRequestModel> validator)
        {
            _articleService = articleService;
            _validator = validator;

        }

        /// <summary>
        /// Returns a list of filtered articles
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArticleViewModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ArticleViewModel>> Get( CancellationToken cancellationToken)
        {
            return await _articleService.GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ArticleViewModel> Get(int id, CancellationToken cancellationToken)
        {
            return await _articleService.GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Creates an entity
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ArticleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ArticleViewModel> Add([FromBody] AddArticleRequestModel articleModel, CancellationToken cancellationToken)
        {
            return await _articleService.AddAsync(articleModel, cancellationToken);
        }

        /// <summary>
        /// Deletes an entity by its ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _articleService.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// Updates a given entity
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ArticleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ArticleViewModel> Update([FromBody] AddArticleRequestModel articleModel, CancellationToken cancellationToken)
        {
            return await _articleService.UpdateAsync(articleModel, cancellationToken);
        }
    }
}
