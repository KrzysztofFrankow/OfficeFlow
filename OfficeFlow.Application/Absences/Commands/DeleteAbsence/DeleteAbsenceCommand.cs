using MediatR;

namespace OfficeFlow.Application.Absences.Commands.DeleteAbsence
{
    public class DeleteAbsenceCommand : IRequest
    {
        public Guid PublicId { get; set; }
        public DeleteAbsenceCommand(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
