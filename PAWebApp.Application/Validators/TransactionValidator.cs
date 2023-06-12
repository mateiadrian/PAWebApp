using FluentValidation;
using PAWebApp.Application.Models.Transactions;
using PAWebApp.Infrastructure.Repositories.CustomerRepository;

namespace PAWebApp.Application.Validators
{
    public class TransactionValidator : AbstractValidator<AddTransactionRequestModel>
    {
        private readonly ICustomerRepository _customerRepository;

        public TransactionValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(a => a.CustomerId)
                .MustAsync(_customerRepository.CustomerIdExists)
                .WithMessage("Invalid customer id");

            RuleFor(a => a.TotalPrice)
                    .GreaterThan(0);

            RuleFor(a => a.Articles)
                .NotEmpty();
        }
    }
}
