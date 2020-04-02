using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace TA.FluentValidation.Extensions.Options.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddStartupFilterToValidateOptionsWithFluentValidation_ThisIsNull_Throw()
        {
            Action action = () => ServiceCollectionExtensions.AddStartupFilterToValidateOptionsWithFluentValidation(null);
            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("services");
        }

        [Fact]
        public void AddStartupFilterToValidateOptionsWithFluentValidation_Call_ReturnThis()
        {
            var servicesMock = new Mock<IServiceCollection>();

            servicesMock.Object.AddStartupFilterToValidateOptionsWithFluentValidation()
                .Should().BeSameAs(servicesMock.Object);
        }

        [Fact]
        public void AddStartupFilterToValidateOptionsWithFluentValidation_Call_RegisterStartupFilter()
        {
            var servicesMock = new Mock<IServiceCollection>();
            servicesMock.Setup(el => el.Add(It.Is<ServiceDescriptor>(
                el1 => el1.ServiceType == typeof(IStartupFilter) &&
                el1.ImplementationType == typeof(ValidateOptionsWithFluentValidationStartupFilter) &&
                el1.Lifetime == ServiceLifetime.Transient)));

            servicesMock.Object.AddStartupFilterToValidateOptionsWithFluentValidation();

            servicesMock.Verify(el => el.Add(It.IsAny<ServiceDescriptor>()), Times.Exactly(1));
        }
    }
}
