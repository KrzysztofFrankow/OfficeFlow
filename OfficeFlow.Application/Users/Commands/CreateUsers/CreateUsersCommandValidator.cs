using FluentValidation;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Commands.CreateUsers
{
    public class CreateUsersCommandValidator : AbstractValidator<CreateUsersCommand>
    {
        public CreateUsersCommandValidator(IOfficeFlowRepository officeFlowRepository)
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Imię jest wymagane.")
                .MinimumLength(2).WithMessage("Imię nie może mieć mniej niż 2 znaki.")
                .MaximumLength(40).WithMessage("Imię nie może mieć więcej niż 40 znaków.");

            RuleFor(c => c.SecondName)
                .MinimumLength(2).When(c => !string.IsNullOrEmpty(c.Email), ApplyConditionTo.CurrentValidator).WithMessage("Drugie imię nie może mieć mniej niż 2 znaki.")
                .MaximumLength(40).When(c => !string.IsNullOrEmpty(c.Email), ApplyConditionTo.CurrentValidator).WithMessage("Drugie imię nie może mieć więcej niż 40 znaków.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Naswisko jest wymagane.")
                .MinimumLength(2).WithMessage("Nazwisko nie może mieć mniej niż 2 znaki.")
                .MaximumLength(40).WithMessage("Nazwisko nie może mieć więcej niż 40 znaków.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Adres e-mail jest wymagany.")
                .EmailAddress().When(c => !string.IsNullOrEmpty(c.Email), ApplyConditionTo.CurrentValidator).WithMessage("Email jest nieprawidłowy.")
                .Custom((value, context) =>
                {
                    if(officeFlowRepository.CheckEmail(value))
                    {
                        context.AddFailure("Email", "Podany adres e-mail jest już w użyciu.");
                    }
                });

            RuleFor(c => c.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Numer telefonu jest nieprawidłowy.");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("Data urodzenia jest wymagana.");

            RuleFor(c => c.RoleId)
                .NotEmpty().WithMessage("Rola jest wymagana.");

            RuleFor(c => c.Password)
                .MinimumLength(6)
                .NotEmpty().WithMessage("Hasło jest wymagane.");

            RuleFor(c => c.ConfirmPassword)
                .Equal(e => e.Password);
        }
    }
}
