using FluentValidation;
using PAWebApp.Application.Models.Customers;
using PAWebApp.Application.Validators.ValidationMessages;

namespace PAWebApp.Application.Validators
{
    public class CustomerValidator : AbstractValidator<AddCustomerRequestModel>
    {
        private static readonly int _nameMaxLength = 100;
        private static readonly int _addressMaxLength = 250;

        public CustomerValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage($"{ValidationMessage.IsRequired}")
                .MaximumLength(_nameMaxLength).WithMessage($"{ValidationMessage.MaxLength} {_nameMaxLength}");

            RuleFor(a => a.Address)
                .NotEmpty().WithMessage($"{ValidationMessage.IsRequired}")
                .MaximumLength(_addressMaxLength).WithMessage($"{ValidationMessage.MaxLength} {_nameMaxLength}");
        }
    }
}
