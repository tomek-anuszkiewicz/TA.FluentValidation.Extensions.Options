using FluentValidation;
using Microsoft.Extensions.Options;

namespace TA.FluentValidation.Extensions.Options
{
    internal class ValidateOptionsWithFluentValidation<TOptions> : IValidateOptions<TOptions>
        where TOptions : class
    {
        private readonly IValidator<TOptions> _validator;

        public ValidateOptionsWithFluentValidation(IValidator<TOptions> validator)
        {
            _validator = validator;
        }

        public ValidateOptionsResult Validate(string name, TOptions options)
        {
            return _validator.Validate(options).ToValidateOptionsResult();
        }
    }
}