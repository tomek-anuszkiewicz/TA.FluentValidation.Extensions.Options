using FluentValidation;

namespace ValidationInController
{
    public class PersonSettingsValidator : AbstractValidator<PersonSettings>
    {
        public PersonSettingsValidator()
        {
            RuleFor(el => el.Age)
                .GreaterThanOrEqualTo(18);

            RuleFor(el => el.Weight)
                .GreaterThanOrEqualTo(50);

            RuleFor(el => el.Height)
                .GreaterThanOrEqualTo(1.5m);
        }
    }
}
