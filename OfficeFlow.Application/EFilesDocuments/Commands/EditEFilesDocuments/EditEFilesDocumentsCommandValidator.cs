using FluentValidation;

namespace OfficeFlow.Application.EFilesDocuments.Commands.EditEFilesDocuments
{
    public class EditEFilesDocumentsCommandValidator : AbstractValidator<EditEFilesDocumentsCommand>
    {
        public EditEFilesDocumentsCommandValidator()
        {
            RuleFor(c => c.Date)
                .NotEmpty().WithMessage("Data dokumentu jest wymagana.");
        }
    }
}
