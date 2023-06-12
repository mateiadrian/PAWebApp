using PAWeb.Domain.Entities;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.CustomerRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> CustomerIdExists(int id, CancellationToken cancellationToken);
        void SeedData();
    }
}
