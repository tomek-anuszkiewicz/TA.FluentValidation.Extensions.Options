using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace TA.FluentValidation.Extensions.Options.Tests
{
    public class OptionsBuilderExtensionsTests
    {
        private class TestOptions
        {
        }

        [Fact]
        public void ValidateWithFluentValidation_ThisIsNull_Throw()
        {
            Action action = () => OptionsBuilderExtensions.ValidateWithFluentValidation<TestOptions>(null);
            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("optionsBuilder");
        }

        [Fact]
        public void ValidateWithFluentValidation_CallValidateWithFluentValidation_ReturnThis()
        {
            var servicesMock = new Mock<IServiceCollection>();
            var optionsBuilder = new OptionsBuilder<TestOptions>(servicesMock.Object, "name1");

            optionsBuilder.ValidateWithFluentValidation().Should().BeSameAs(optionsBuilder);
        }

        [Fact]
        public void ValidateWithFluentValidation_CallValidateWithFluentValidation_RegisterValidateOptions()
        {
            var servicesMock = new Mock<IServiceCollection>();
            servicesMock.Setup(el => el.Add(It.Is<ServiceDescriptor>(
                el1 => el1.ServiceType == typeof(IValidateOptions<TestOptions>) &&
                el1.ImplementationType == typeof(ValidateOptionsWithFluentValidation<TestOptions>) &&
                el1.Lifetime == ServiceLifetime.Singleton)));

            var optionsBuilder = new OptionsBuilder<TestOptions>(servicesMock.Object, "name1");

            optionsBuilder.ValidateWithFluentValidation();

            servicesMock.Verify(
                el => el.Add(
                    It.Is<ServiceDescriptor>(el1 => el1.ServiceType == typeof(IValidateOptions<TestOptions>) &&
                    el1.ImplementationType == typeof(ValidateOptionsWithFluentValidation<TestOptions>) &&
                    el1.Lifetime == ServiceLifetime.Singleton)),
                Times.Once);

            servicesMock.Verify(el => el.Add(It.IsAny<ServiceDescriptor>()), Times.Exactly(2));
        }

        [Fact]
        public void ValidateWithFluentValidation_CallValidateWithFluentValidation_RegisterValidateOptionsWithFluentValidationOnStartup()
        {
            var servicesMock = new Mock<IServiceCollection>();
            servicesMock.Setup(el => el.Add(It.Is<ServiceDescriptor>(
                el1 => el1.ServiceType == typeof(IValidateOptionsWithFluentValidationOnStartup) &&
                el1.ImplementationType == typeof(ValidateOptionsWithFluentValidationOnStartup<TestOptions>) &&
                el1.Lifetime == ServiceLifetime.Singleton)));

            var optionsBuilder = new OptionsBuilder<TestOptions>(servicesMock.Object, "name1");

            optionsBuilder.ValidateWithFluentValidation();

            servicesMock.Verify(el =>
                el.Add(It.Is<ServiceDescriptor>(
                    el1 => el1.ServiceType == typeof(IValidateOptionsWithFluentValidationOnStartup) &&
                    el1.ImplementationType == typeof(ValidateOptionsWithFluentValidationOnStartup<TestOptions>) &&
                    el1.Lifetime == ServiceLifetime.Singleton)),
                Times.Once);

            servicesMock.Verify(el => el.Add(It.IsAny<ServiceDescriptor>()), Times.Exactly(2));
        }
    }
}
