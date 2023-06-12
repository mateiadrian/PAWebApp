using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using NUnit.Framework;
using PAWeb.Domain.Entities;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Application.Models.Transactions;
using PAWebApp.Application.Services.ArticleService;
using PAWebApp.Application.Services.PaymentService;
using PAWebApp.Application.Services.TransactionService.cs;
using PAWebApp.Infrastructure.Repositories.TransactionRepository;

namespace PAWebApp.Tests.UnitTests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private Mock<ITransactionRepository> _transactionRepositoryMock;
        private Mock<IValidator<AddTransactionRequestModel>> _validatorMock;
        private Mock<IArticleService> _articleServiceMock;
        private Mock<IPaymentService> _paymentServiceMock;
        private Mock<IMapper> _mapperMock;

        private TransactionService _transactionService;

        [SetUp]
        public void Setup()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _validatorMock = new Mock<IValidator<AddTransactionRequestModel>>();
            _articleServiceMock = new Mock<IArticleService>();
            _paymentServiceMock = new Mock<IPaymentService>();
            _mapperMock = new Mock<IMapper>();

            _transactionService = new TransactionService(
                _transactionRepositoryMock.Object,
                _mapperMock.Object,
                _validatorMock.Object,
                _articleServiceMock.Object,
                _paymentServiceMock.Object
            );
        }

        [Test]
        public async Task AddAsync_ValidTransactionModel_ReturnsTransactionViewModel()
        {
            var transactionModel = new AddTransactionRequestModel();
            var cancellationToken = CancellationToken.None;

            _validatorMock.Setup(v => v.Validate(transactionModel)).Returns(new ValidationResult(null));

            _transactionRepositoryMock
                .Setup(r => r.CreateTransactionAsync(cancellationToken))
                .ReturnsAsync(Mock.Of<IDbContextTransaction>());

            _mapperMock
                .Setup(m => m.Map<AddTransactionRequestModel>(transactionModel))
                .Returns(transactionModel);

            // Act
            var result = await _transactionService.AddAsync(transactionModel, cancellationToken);

            // Assert
            Assert.AreEqual(transactionModel, result);
        }

        [Test]
        public async Task AddAsync_ExceptionWhenProcessing_TransactionDisposedAndExceptionRethrown()
        {
            var transactionViewModel = new AddTransactionRequestModel();
            var cancellationToken = CancellationToken.None;

            _validatorMock.Setup(v => v.Validate(transactionViewModel)).Returns(new ValidationResult());
            _transactionRepositoryMock
                .Setup(r => r.CreateTransactionAsync(cancellationToken))
                .ReturnsAsync(Mock.Of<IDbContextTransaction>());

            _mapperMock
               .Setup(m => m.Map<Transaction>(transactionViewModel))
               .Returns(new Transaction());

            _transactionRepositoryMock
                .Setup(r => r.AddAsync(new Transaction(), cancellationToken))
                .ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(async () => await _transactionService.AddAsync(transactionViewModel, cancellationToken));
        }

        [Test]
        public async Task UpdateArticlesInventory_ValidTransactionArticles_ArticleInventoryUpdated()
        {
            var transactionArticles = new List<TransactionArticleModel>
            {
                new TransactionArticleModel { ArticleId = 1, Quantity = 2 },
                new TransactionArticleModel { ArticleId = 2, Quantity = 1 }
            };

            var cancellationToken = CancellationToken.None;

            var articles = new List<ArticleViewModel>
            {
                new ArticleViewModel { Id = 1, Inventory = 10 },
                new ArticleViewModel { Id = 2, Inventory = 5 }
            };

            _articleServiceMock
                .Setup(a => a.GetByIds(It.IsAny<List<int>>(), cancellationToken))
                    .ReturnsAsync(articles);

            _articleServiceMock
                .Setup(a => a.UpdateAsync(It.IsAny<AddArticleRequestModel>(), cancellationToken))
                    .Returns((Task<ArticleViewModel>)Task.CompletedTask);

            await _transactionService.UpdateArticlesInventory(transactionArticles, cancellationToken);

            Assert.AreEqual(8, articles[0].Inventory);
            Assert.AreEqual(4, articles[1].Inventory);
        }
    }
}
