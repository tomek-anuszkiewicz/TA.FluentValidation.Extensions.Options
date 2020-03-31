using System.Linq;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace TA.FluentValidation.Extensions.Options
{
    internal static class FluentValidationValidationResultExtensions
    {
        public static ValidateOptionsResult ToValidateOptionsResult(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return ValidateOptionsResult.Success;
            else
                return ValidateOptionsResult.Fail(validationResult.Errors.Select(el => el.ErrorMessage));
        }
    }
}