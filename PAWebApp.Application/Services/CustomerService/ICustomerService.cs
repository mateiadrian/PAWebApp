using PAWebApp.Application.Models.Customers;

namespace PAWebApp.Application.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<CustomerViewModel> GetByIdAsync(int articleId, CancellationToken cancellationToken);
        Task<CustomerViewModel> AddAsync(AddCustomerRequestModel articleModel, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<CustomerViewModel> UpdateAsync(AddCustomerRequestModel customerMdel, CancellationToken cancellationToken);
    }
}
