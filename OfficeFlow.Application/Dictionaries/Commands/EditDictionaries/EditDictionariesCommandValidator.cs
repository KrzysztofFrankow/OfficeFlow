using FluentValidation;

namespace OfficeFlow.Application.Dictionaries.Commands.EditDictionaries
{
    public class EditDictionariesCommandValidator : AbstractValidator<EditDictionariesCommand>
    {
        public EditDictionariesCommandValidator()
        {
            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Rodzaj nieobecności jest wymagany.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nazwa jest wymagana.");
        }
    }
}
