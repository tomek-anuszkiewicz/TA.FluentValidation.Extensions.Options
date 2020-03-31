using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TA.FluentValidation.Extensions.Options
{
    /// <summary>
    /// Extensions to validate options with <see cref="global::FluentValidation.IValidator{TOptions}"/>.
    /// </summary>
    public static class OptionsBuilderExtensions
    {
        /// <summary>
        /// Validate <typeparamref name="TOptions"/> with <see cref="global::FluentValidation.IValidator{TOptions}"/>.<br/>
        /// </summary>
        /// <typeparam name="TOptions">Class to be validated.</typeparam>
        /// <param name="optionsBuilder">The options type to be configured.</param>
        /// <returns>
        /// The <see cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}"/>
        /// so that additional calls can be chained.
        /// </returns>
        public static OptionsBuilder<TOptions> ValidateWithFluentValidation<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
            where TOptions : class, new()
        {
            if (optionsBuilder is null)
                throw new System.ArgumentNullException(nameof(optionsBuilder));

            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>, ValidateOptionsWithFluentValidation<TOptions>>();
            optionsBuilder.Services.AddSingleton<IValidateOptionsWithFluentValidationOnStartup,
                ValidateOptionsWithFluentValidationOnStartup<TOptions>>();
            return optionsBuilder;
        }
    }
}