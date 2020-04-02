using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace TA.FluentValidation.Extensions.Options
{
    internal class ValidateOptionsWithFluentValidationStartupFilter : IStartupFilter
    {
        private readonly IEnumerable<IValidateOptionsWithFluentValidationOnStartup> _startupValidators;

        public ValidateOptionsWithFluentValidationStartupFilter(IEnumerable<IValidateOptionsWithFluentValidationOnStartup> startupValidators)
        {
            _startupValidators = startupValidators ?? throw new ArgumentNullException(nameof(startupValidators));
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var validator in _startupValidators)
                validator.Validate();

            return next;
        }
    }
}