using FluentAssertions;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace TA.FluentValidation.Extensions.Options.Tests
{
    public class ValidateOptionsWithFluentValidationTests
    {
        private class TestOptions
        {
            public int TestProp { get; set; } = 3;
        }

        private class TestOptionsValidator : AbstractValidator<TestOptions>
        {
            public TestOptionsValidator()
            {
                RuleFor(el => el.TestProp).Equal(4);
            }
        }

        [Fact]
        [SuppressMessage("Usage", "CA1806:Do not ignore method results", Justification = "Testing side affects in constructor")]
        public void Constructor_WithNull_Throw()
        {
            Action action = () => new ValidateOptionsWithFluentValidation<TestOptions>(null);
            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("validator");
        }

        [Fact]
        public void ValidateOptionsWithFluentValidationTests_Validate_ValidateResult()
        {
            var testOptions = new TestOptions();
            var testOptionsValidator = new TestOptionsValidator();
            var validateOptionsWithFluentValidation = new ValidateOptionsWithFluentValidation<TestOptions>(testOptionsValidator);
            var validateOptionsResult = validateOptionsWithFluentValidation.Validate(null, testOptions);

            validateOptionsResult.Failed.Should().BeTrue();
            validateOptionsResult.FailureMessage.Should().Be("'Test Prop' should be equal to '4'.");
        }
    }
}
