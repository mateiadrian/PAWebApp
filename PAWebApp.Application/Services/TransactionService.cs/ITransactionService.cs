using PAWebApp.Application.Models.Transactions;

namespace PAWebApp.Application.Services.TransactionService.cs
{
    public interface ITransactionService
    {
        Task<AddTransactionRequestModel> AddAsync(AddTransactionRequestModel transactionModel, CancellationToken cancellationToken);
        Task<IEnumerable<AddTransactionRequestModel>> GetAllAsync(CancellationToken cancellationToken);
    }
}
