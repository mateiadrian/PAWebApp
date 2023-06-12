using Microsoft.EntityFrameworkCore;
using PAWeb.Domain.Entities;
using PAWeb.Infrastructure;
using PAWebApp.Domain.Entities;
using PAWebApp.Infrastructure.Repositories.BaseRepository;

namespace PAWebApp.Infrastructure.Repositories.CustomerRepository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


        public async Task<bool> CustomerIdExists(int id, CancellationToken cancellationToken)
        {
           return await _dbContext.Customers.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new Customer { Id = 1, Name = "Adrian", Address = "Cluj Napoca" },
                    new Customer { Id = 2, Name = "Karina", Address = "Oradea" },
                    new Customer { Id = 3, Name = "Matei", Address = "Bucuresti" },
                    new Customer { Id = 4, Name = "Matei", Address = "Bucuresti",
                            Transactions = new List<Transaction>
                            {
                                new Transaction {
                                    Id = 1,
                                    TransactionArticles = new List<TransactionArticle>
                                    {
                                        new TransactionArticle { ArticleId = 1, TransactionId = 1 },
                                    },
                                    TotalArticles = 1,
                                    TotalPrice = 10,
                                }
                            }
                            }
                };

                _dbContext.Customers.AddRange(customers);
                _dbContext.SaveChanges();
            }
        }
    }
}
