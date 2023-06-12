using FluentValidation;
using PAWebApp.Application.Models.Payments;
using PAWebApp.Application.Validators.ValidationMessages;

namespace PAWebApp.Application.Validators
{
    public class PaymentValidator: AbstractValidator<AddPaymentRequestModel>
    {
        public PaymentValidator()
        {
            RuleFor(a => a.Price)
                .GreaterThan(0).WithMessage($"{ValidationMessage.GreaterThan} 0");

            RuleFor(a => a.Type)
                .IsInEnum();

            RuleFor(a => a.Currency)
                .NotEmpty();
        }
    }
}
