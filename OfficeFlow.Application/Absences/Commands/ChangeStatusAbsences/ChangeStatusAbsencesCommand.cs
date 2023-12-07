using MediatR;


namespace OfficeFlow.Application.Absences.Commands.ChangeStatusAbsences
{
    public class ChangeStatusAbsencesCommand :  IRequest
    {
        public Guid PublicId { get; set; }
        public int Status { get; set; }
        public ChangeStatusAbsencesCommand(Guid publicId, int status)
        {
            PublicId = publicId;
            Status = status;
        }
    }
}
