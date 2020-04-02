using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TA.FluentValidation.Extensions.Options
{
    /// <summary>
    /// Extensions to validate <see cref="Microsoft.Extensions.Options.IOptions{TOptions}"/>
    /// with <see cref="global::FluentValidation.IValidator{TOptions}"/> on startup.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Validate on application startup all <see cref="Microsoft.Extensions.Options.IOptions{TOptions}"/>
        /// marked for validation with
        /// <see cref="OptionsBuilderExtensions.ValidateWithFluentValidation{TOptions}(Microsoft.Extensions.Options.OptionsBuilder{TOptions})"/> .
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
        /// <returns>
        ///  An Microsoft.Extensions.DependencyInjection.IMvcBuilder that can be used to further configure the MVC services.
        /// </returns>
        public static IServiceCollection AddStartupFilterToValidateOptionsWithFluentValidation(this IServiceCollection services)
        {
            if (services is null)
                throw new System.ArgumentNullException(nameof(services));

            services.AddTransient<IStartupFilter, ValidateOptionsWithFluentValidationStartupFilter>();
            return services;
        }
    }
}