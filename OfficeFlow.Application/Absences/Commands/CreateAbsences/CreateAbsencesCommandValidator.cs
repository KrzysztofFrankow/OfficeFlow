using FluentValidation;
using OfficeFlow.Application.Users.Commands.CreateUsers;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Absences.Commands.CreateAbsences
{
    public class CreateAbsencesCommandValidator : AbstractValidator<CreateAbsencesCommand>
    {
        public CreateAbsencesCommandValidator()
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
