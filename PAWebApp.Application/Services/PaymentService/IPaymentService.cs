using PAWebApp.Application.Models.Payments;

namespace PAWebApp.Application.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<PaymentViewModel> GetByIdAsync(int paymentId, CancellationToken cancellationToken);
        Task<PaymentViewModel> AddAsync(AddPaymentRequestModel paymentModel, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<PaymentViewModel> UpdateAsync(AddPaymentRequestModel paymentModel, CancellationToken cancellationToken);
    }
}
