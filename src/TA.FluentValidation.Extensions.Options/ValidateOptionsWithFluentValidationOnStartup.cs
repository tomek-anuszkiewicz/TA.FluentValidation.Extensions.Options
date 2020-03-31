using System;
using Microsoft.Extensions.Options;

namespace TA.FluentValidation.Extensions.Options
{
    internal class ValidateOptionsWithFluentValidationOnStartup<TOptions> : IValidateOptionsWithFluentValidationOnStartup
        where TOptions : class, new()
    {
        private readonly IOptions<TOptions> _options;

        public ValidateOptionsWithFluentValidationOnStartup(IOptions<TOptions> options)
        {
            _options = options;
        }

        public void Validate()
        {
            TriggerValidation();
        }

        private void TriggerValidation()
        {
            _options.Value.GetType();
        }
    }
}