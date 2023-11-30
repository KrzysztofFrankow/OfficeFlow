using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFiles.Commands.EditEFiles
{
    public class EditEFilesCommandHandler : IRequestHandler<EditEFilesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public EditEFilesCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(EditEFilesCommand request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetEFileByPublicId(request.PublicId);
            user.FolderNumber = request.FolderNumber;
            user.StorageLocation = request.StorageLocation;
            user.Notes = request.Notes;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
