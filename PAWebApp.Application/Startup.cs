using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PAWebApp.Application.AutoMapperProfiles;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Models.Customers;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Application.Models.Transactions;
using PAWebApp.Application.Services.ArticleService;
using PAWebApp.Application.Services.CustomerService;
using PAWebApp.Application.Services.PaymentService;
using PAWebApp.Application.Services.TransactionService.cs;
using PAWebApp.Application.Validators;

namespace PAWebApp.Application
{
    public static class Startup
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IValidator<AddArticleRequestModel>, ArticleValidator>();
            services.AddScoped<IValidator<AddCustomerRequestModel>, CustomerValidator>();
            services.AddScoped<IValidator<AddPaymentRequestModel>, PaymentValidator>();
            services.AddScoped<IValidator<AddTransactionRequestModel>, TransactionValidator>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
                cfg.AllowNullCollections = true;
            });
        }
    }
}
