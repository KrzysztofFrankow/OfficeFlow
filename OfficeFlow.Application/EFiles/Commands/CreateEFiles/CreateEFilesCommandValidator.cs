using FluentValidation;

namespace OfficeFlow.Application.EFiles.Commands.CreateEFiles
{
    public class CreateEFilesCommandValidator : AbstractValidator<CreateEFilesCommand>
    {
        public CreateEFilesCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Użytkownik jest wymagany.");
        }
    }
}
