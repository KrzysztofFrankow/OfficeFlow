using FluentValidation;

namespace OfficeFlow.Application.Users.Commands.EditUsers
{
    public class EditUsersCommandValidator : AbstractValidator<EditUsersCommand>
    {
        public EditUsersCommandValidator()
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
                .EmailAddress().When(c => !string.IsNullOrEmpty(c.Email), ApplyConditionTo.CurrentValidator).WithMessage("Email jest nieprawidłowy.");

            RuleFor(c => c.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Numer telefonu jest nieprawidłowy.");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("Data urodzenia jest wymagana.");

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Osoba wprowadzająca jest wymagana.");
        }
    }
}
