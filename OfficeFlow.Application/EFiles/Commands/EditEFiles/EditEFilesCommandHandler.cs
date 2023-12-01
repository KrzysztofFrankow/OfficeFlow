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
            var eFile = await _officeFlowRepository.GetEFileByPublicId(request.PublicId);
            eFile.FolderNumber = request.FolderNumber;
            eFile.StorageLocation = request.StorageLocation;
            eFile.Notes = request.Notes;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
