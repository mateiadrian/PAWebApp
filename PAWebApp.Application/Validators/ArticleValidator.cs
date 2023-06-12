using FluentValidation;
using PAWebApp.Application.Models.Articles;
using PAWebApp.Application.Validators.ValidationMessages;

namespace PAWebApp.Application.Validators
{
    public class ArticleValidator : AbstractValidator<AddArticleRequestModel>
    {
        private static readonly int _nameMaxLength = 100;

        public ArticleValidator()
        {
            // should a commom Validation Messages class for messages like :IsRequired, MaxLength, Conflict
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage($"{ValidationMessage.IsRequired}")
                .MaximumLength(_nameMaxLength).WithMessage($"{ValidationMessage.MaxLength} {_nameMaxLength}");

            RuleFor(a => a.Quantity)
                .NotEmpty();

            RuleFor(a => a.Price)
                .NotEmpty();
        }
    }
}
