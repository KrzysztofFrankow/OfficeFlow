using MediatR;
using OfficeFlow.Application.EFiles.Commands.EditEFiles;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Dictionaries.Commands.EditDictionaries
{
    public class EditDictionariesCommandHandler : IRequestHandler<EditDictionariesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public EditDictionariesCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(EditDictionariesCommand request, CancellationToken cancellationToken)
        {
            var Dictionary = await _officeFlowRepository.GetDictionaryById(request.Id);
            Dictionary.Type = request.Type;
            Dictionary.Name = request.Name;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
