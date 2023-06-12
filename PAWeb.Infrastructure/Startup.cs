using Microsoft.Extensions.DependencyInjection;
using PAWebApp.Infrastructure.Repositories.ArticleRepository;
using PAWebApp.Infrastructure.Repositories.CustomerRepository;
using PAWebApp.Infrastructure.Repositories.PaymentRepository;
using PAWebApp.Infrastructure.Repositories.TransactionRepository;

namespace PAWeb.Infrastructure
{
    public static class Startup
    {
        public static void ConfigureAppInfrastucture(this IServiceCollection services)
        {
            //should contain adding the DbContext, but since we use in memory I left it out

            //services.AddDbContext<DatabaseContext>(options =>
            //{
            //    string? connectionString = configuration.GetConnectionString("ConnectionString");
            //    options.UseSqlServer(connectionString);
            //});

            services.AddDbContext<DatabaseContext>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        public static void SeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();
                articleRepository.SeedData();

                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                customerRepository.SeedData();

                var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();
                paymentRepository.SeedData();
            }
        }
    }
}
