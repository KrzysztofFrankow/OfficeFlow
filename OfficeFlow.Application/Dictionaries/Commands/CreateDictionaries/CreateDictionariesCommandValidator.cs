using FluentValidation;

namespace OfficeFlow.Application.Dictionaries.Commands.CreateDictionaries
{
    public class CreateDictionariesCommandValidator : AbstractValidator<CreateDictionariesCommand>
    {
        public CreateDictionariesCommandValidator()
        {
            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Rodzaj nieobecności jest wymagany.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nazwa jest wymagana.");
        }
    }
}
