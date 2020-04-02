using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace TA.FluentValidation.Extensions.Options.Tests
{
    public class ValidateOptionsWithFluentValidationOnStartupTests
    {
        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Must be public to create mock")]
        public class TestOptions
        {
        }

        [Fact]
        [SuppressMessage("Usage", "CA1806:Do not ignore method results", Justification = "Testing side affects in constructor")]
        public void Constructor_WithNull_Throw()
        {
            Action action = () => new ValidateOptionsWithFluentValidationOnStartup<TestOptions>(null);
            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
        }

        [Fact]
        public void Validate_Validate_GetValue()
        {
            var optionsMock = new Mock<IOptions<TestOptions>>(MockBehavior.Strict);
            optionsMock.SetupGet(el => el.Value).Returns(new TestOptions());
            var validateOptionsWithFluentValidationOnStartup = new ValidateOptionsWithFluentValidationOnStartup<TestOptions>(optionsMock.Object);

            validateOptionsWithFluentValidationOnStartup.Validate();

            optionsMock.VerifyGet(el => el.Value, Times.Once);
        }
    }
}
