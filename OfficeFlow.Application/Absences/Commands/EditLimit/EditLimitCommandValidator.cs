using FluentValidation;
using OfficeFlow.Application.Absences.Commands.CreateLimit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Absences.Commands.EditLimit
{
    public class EditLimitCommandValidator : AbstractValidator<EditLimitCommand>
    {
        public EditLimitCommandValidator()
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
