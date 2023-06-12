using FluentValidation.Results;
using System.Net;

namespace PAWebApp.Application.Exceptions
{
    [Serializable]
    public class ModelValidationException : BaseHttpException
    {
        public ModelValidationException(List<ValidationFailure> errors)
            : base(HttpStatusCode.BadRequest, FormatErrors(errors))
        {
        }

        private static string FormatErrors(List<ValidationFailure> errors)
        {
            var propertyErrors = errors
                .GroupBy(x => x.PropertyName)
                .Select(FormatPropertyErrors);

            return string.Join("; ", propertyErrors);
        }

        private static string FormatPropertyErrors(IGrouping<string, ValidationFailure> group)
        {
            return $"{group.Key}: {string.Join(", ", group.Select(x => x.ErrorMessage))}";
        }
    }
}
