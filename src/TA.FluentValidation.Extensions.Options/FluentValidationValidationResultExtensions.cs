using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System.Linq;

namespace TA.FluentValidation.Extensions.Options
{
    internal static class FluentValidationValidationResultExtensions
    {
        public static ValidateOptionsResult ToValidateOptionsResult(this ValidationResult validationResult)
        {
            if (validationResult is null)
                throw new System.ArgumentNullException(nameof(validationResult));

            if (validationResult.IsValid)
                return ValidateOptionsResult.Success;
            else
                return ValidateOptionsResult.Fail(validationResult.Errors.Select(el => el.ErrorMessage));
        }
    }
}