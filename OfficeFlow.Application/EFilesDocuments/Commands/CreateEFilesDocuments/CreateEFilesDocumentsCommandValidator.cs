using FluentValidation;

namespace OfficeFlow.Application.EFilesDocuments.Commands.CreateEFilesDocuments
{
    public class CreateEFilesDocumentsCommandValidator : AbstractValidator<CreateEFilesDocumentsCommand>
    {
        public CreateEFilesDocumentsCommandValidator()
        {
            RuleFor(c => c.Date)
                .NotEmpty().WithMessage("Data dokumentu jest wymagana.");
        }
    }
}
