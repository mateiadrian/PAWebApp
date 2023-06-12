using PAWeb.Domain.Entities;
using PAWeb.Infrastructure;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.PaymentRepository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DatabaseContext dbContext) : base(dbContext)
        {

        }

        public void SeedData()
        {
            if (!_dbContext.Payments.Any())
            {
                var payments = new List<Payment>()
                {
                    new Payment() { Id = 1, Currency = "EUR", Price = 1200, Type = PAWeb.Domain.Enums.PaymentType.Cash, CustomerId = 1 },
                    new Payment() { Id = 2, Currency = "EUR", Price = 1100, Type = PAWeb.Domain.Enums.PaymentType.Card, CustomerId = 2 }
                };

                _dbContext.Payments.AddRange(payments);
                _dbContext.SaveChanges();
            }
        }
    }
}
