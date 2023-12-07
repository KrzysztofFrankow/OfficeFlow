using FluentValidation;

namespace OfficeFlow.Application.Absences.Commands.CreateLimit
{
    public class CreateLimitCommandValidator : AbstractValidator<CreateLimitCommand>
    {
        public CreateLimitCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Pracownik jest wymagany.");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Rodzaj nieobecności jest wymagany.");

            RuleFor(c => c.DaysLimit)
                .NotEmpty().WithMessage("Limit dniowy jest wymagany.");
        }
    }
}
