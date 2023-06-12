using AutoMapper;
using FluentValidation;
using PAWeb.Domain.Entities;
using PAWebApp.Application.Exceptions;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Application.Models.Transactions;
using PAWebApp.Application.Services.ArticleService;
using PAWebApp.Application.Services.PaymentService;
using PAWebApp.Infrastructure.Repositories.TransactionRepository;

namespace PAWebApp.Application.Services.TransactionService.cs
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddTransactionRequestModel> _validator;
        private readonly IArticleService _articleService;
        private readonly IPaymentService _paymentService;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper,
               IValidator<AddTransactionRequestModel> validator, IArticleService articleService, IPaymentService paymentService)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _validator = validator;
            _articleService = articleService;
            _paymentService = paymentService;
        }

        public async Task<AddTransactionRequestModel> AddAsync(AddTransactionRequestModel addTransactionModel, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(addTransactionModel);

            if(validationResult.IsValid)
            {
                // not working with in memory db context
                //using var transaction = await _transactionRepository.CreateTransactionAsync(cancellationToken);

                try
                {
                    await UpdateArticlesInventory(addTransactionModel.Articles, cancellationToken);
                    await AddPayment(addTransactionModel.Payment, cancellationToken);
                    var transactionModel = await AddTransaction(addTransactionModel, cancellationToken);

                    //await transaction.CommitAsync(cancellationToken);

                    return _mapper.Map<AddTransactionRequestModel>(transactionModel);
                }
                catch (BaseHttpException ex)
                {
                    //transaction.Dispose();
                    throw;
                }
            }

            throw new ModelValidationException(validationResult.Errors);
        }

        public async Task UpdateArticlesInventory(IList<TransactionArticleModel> transactionArticles, CancellationToken cancellationToken)
        {
            var articlesIds = transactionArticles.Select(a => a.ArticleId).ToList();
            var articles = await _articleService.GetByIds(articlesIds, cancellationToken);

            foreach (var transactionArticle in transactionArticles)
            {
                var article = articles.FirstOrDefault(a => a.Id == transactionArticle.ArticleId);

                if (article == null)
                {
                    //case 1: throw exception
                    //case 2: ask for re-scan of products
                    //ask cashier/user to remove article and update total price
                }

                article.Inventory -= transactionArticle.Quantity;

                var articleDbModel = _mapper.Map<ArticleViewModel, AddArticleRequestModel>(article);

                await _articleService.UpdateAsync(articleDbModel, cancellationToken);
            }
        }

        public async Task<IEnumerable<AddTransactionRequestModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetAllAsync(cancellationToken);

            return transactions.Select(_mapper.Map<Transaction, AddTransactionRequestModel>).ToList();
        }

        private async Task AddPayment(AddPaymentRequestModel paymentModel, CancellationToken cancellationToken)
        {
            await _paymentService.AddAsync(paymentModel, cancellationToken);
        }

        private async Task<AddTransactionRequestModel> AddTransaction(AddTransactionRequestModel transactionViewModel, CancellationToken cancellationToken)
        {
            var transactionDbModel = _mapper.Map<AddTransactionRequestModel, Transaction>(transactionViewModel); 
            var transactionModel = await _transactionRepository.AddAsync(transactionDbModel, cancellationToken);

            foreach(var article in transactionViewModel.Articles)
            {
                await _transactionRepository.AddTransactionArticle(transactionModel.Id, article.ArticleId, cancellationToken);
            }

            return _mapper.Map<Transaction, AddTransactionRequestModel>(transactionModel);
        }

        
    }
}
