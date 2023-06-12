using PAWeb.Domain.Entities;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.PaymentRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void SeedData();
    }
}
