using FluentAssertions;
using FluentValidation.Results;
using System;
using Xunit;

namespace TA.FluentValidation.Extensions.Options.Tests
{
    public class FluentValidationValidationResultExtensionsTests
    {
        [Fact]
        public void ToValidateOptionsResult_ThisIsNull_Throw()
        {
            Action action = () => FluentValidationValidationResultExtensions.ToValidateOptionsResult(null);
            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("validationResult");
        }

        [Fact]
        public void ToValidateOptionsResult_Success_Successs()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(new ValidationResult());
            validateOptionsResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public void ToValidateOptionsResult_FailureWithOneError_FailureMessage()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(
                new ValidationResult(new[] { new ValidationFailure("p1", "err1") }));

            validateOptionsResult.FailureMessage.Should().Be("err1");
        }

        [Fact]
        public void ToValidateOptionsResult_FailureWithOneError_OneFailure()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(
                new ValidationResult(new[] { new ValidationFailure("p1", "err1") }));

            validateOptionsResult.Failures.Should().HaveCount(1);
        }

        [Fact]
        public void ToValidateOptionsResult_FailureWithOneError_Fail()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(
                new ValidationResult(new[] { new ValidationFailure("p1", "err1") }));

            validateOptionsResult.Failed.Should().BeTrue();
        }

        [Fact]
        public void ToValidateOptionsResult_FailureWithTwoError_ErrorMessage()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(
                new ValidationResult(new[] { new ValidationFailure("p1", "err1"), new ValidationFailure("p2", "err2") }));

            validateOptionsResult.FailureMessage.Should().Be("err1; err2");
        }

        [Fact]
        public void ToValidateOptionsResult_FailureWithTwoError_TwoFailures()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(
                new ValidationResult(new[] { new ValidationFailure("p1", "err1"), new ValidationFailure("p2", "err2") }));

            validateOptionsResult.Failures.Should().HaveCount(2);
        }

        [Fact]
        public void ToValidateOptionsResult_FailureWithTwoError_Fail()
        {
            var validateOptionsResult = FluentValidationValidationResultExtensions.ToValidateOptionsResult(
                new ValidationResult(new[] { new ValidationFailure("p1", "err1"), new ValidationFailure("p2", "err2") }));

            validateOptionsResult.Failed.Should().BeTrue();
        }
    }
}