using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace TA.FluentValidation.Extensions.Options.Tests
{
    public class ValidateOptionsWithFluentValidationStartupFilterTests
    {
        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Must be public to create mock")]
        public class ValidateOptionsWithFluentValidationOnStartup : IValidateOptionsWithFluentValidationOnStartup
        {
            public int Counter { get; set; } = 0;

            public void Validate()
            {
                Counter++;
            }
        }

        [Fact]
        [SuppressMessage("Usage", "CA1806:Do not ignore method results", Justification = "Testing side affects in constructor")]
        public void Constructor_WithNull_Throw()
        {
            Action action = () => new ValidateOptionsWithFluentValidationStartupFilter(null);
            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("startupValidators");
        }

        [Fact]
        public void Configure_Configure_ValidateAll()
        {
            var validateOptionsWithFluentValidationOnStartupMock1 = new ValidateOptionsWithFluentValidationOnStartup();
            var validateOptionsWithFluentValidationOnStartupMock2 = new ValidateOptionsWithFluentValidationOnStartup();

            var startupValidators = new[]
            {
                validateOptionsWithFluentValidationOnStartupMock1,
                validateOptionsWithFluentValidationOnStartupMock2,
            };

            var validateOptionsWithFluentValidationStartupFilter = new ValidateOptionsWithFluentValidationStartupFilter(startupValidators);

            Action<IApplicationBuilder> next = _ => { };

            validateOptionsWithFluentValidationStartupFilter.Configure(next);

            validateOptionsWithFluentValidationOnStartupMock1.Counter.Should().Be(1);
            validateOptionsWithFluentValidationOnStartupMock2.Counter.Should().Be(1);
        }

        [Fact]
        public void Configure_Configure_ReturnNext()
        {
            var validateOptionsWithFluentValidationStartupFilter =
                new ValidateOptionsWithFluentValidationStartupFilter(Array.Empty<IValidateOptionsWithFluentValidationOnStartup>());

            Action<IApplicationBuilder> next = _ => { };

            validateOptionsWithFluentValidationStartupFilter.Configure(next).Should().BeSameAs(next);
        }
    }
}
