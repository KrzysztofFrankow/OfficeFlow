using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFiles.Commands.DeleteEFiles
{
    public class DeleteEFilesCommandHandler : IRequestHandler<DeleteEFilesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public DeleteEFilesCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(DeleteEFilesCommand request, CancellationToken cancellationToken)
        {
            var eFile = await _officeFlowRepository.GetEFileByPublicId(request.PublicId);

            await _officeFlowRepository.Remove(eFile);

            return Unit.Value;
        }
    }
}
