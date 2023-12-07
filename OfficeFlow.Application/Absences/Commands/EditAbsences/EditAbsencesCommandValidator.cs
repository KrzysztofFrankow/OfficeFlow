using FluentValidation;

namespace OfficeFlow.Application.Absences.Commands.EditAbsences
{
    public class EditAbsencesCommandValidator : AbstractValidator<EditAbsencesCommand>
    {
        public EditAbsencesCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Pracownik jest wymagany.");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Rodzaj nieobecności jest wymagany.");

            RuleFor(c => c.From)
                .NotEmpty().WithMessage("Data od jest wymagana.");

            RuleFor(c => c.To)
                .NotEmpty().WithMessage("Data do jest wymagana.");
        }
    }
}
