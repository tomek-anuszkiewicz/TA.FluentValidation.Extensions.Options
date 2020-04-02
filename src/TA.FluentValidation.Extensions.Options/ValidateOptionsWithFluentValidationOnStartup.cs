using Microsoft.Extensions.Options;
using System;

namespace TA.FluentValidation.Extensions.Options
{
    internal class ValidateOptionsWithFluentValidationOnStartup<TOptions> : IValidateOptionsWithFluentValidationOnStartup
        where TOptions : class, new()
    {
        private readonly IOptions<TOptions> _options;

        public ValidateOptionsWithFluentValidationOnStartup(IOptions<TOptions> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
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