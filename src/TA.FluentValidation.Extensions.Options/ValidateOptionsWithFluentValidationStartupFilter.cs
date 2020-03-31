using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace TA.FluentValidation.Extensions.Options
{
    internal class ValidateOptionsWithFluentValidationStartupFilter : IStartupFilter
    {
        private readonly IEnumerable<IValidateOptionsWithFluentValidationOnStartup> _startupValidators;

        public ValidateOptionsWithFluentValidationStartupFilter(IEnumerable<IValidateOptionsWithFluentValidationOnStartup> startupValidators)
        {
            _startupValidators = startupValidators;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var validator in _startupValidators)
                validator.Validate();

            return next;
        }
    }
}