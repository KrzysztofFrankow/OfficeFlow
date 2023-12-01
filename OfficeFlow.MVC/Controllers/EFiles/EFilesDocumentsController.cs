using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.EFilesDocuments.Commands.CreateEFilesDocuments;
using OfficeFlow.Application.EFilesDocuments.Commands.EditEFilesDocuments;
using OfficeFlow.Application.EFilesDocuments.Queries.GetDocumentById;
using OfficeFlow.Application.EFilesDocuments.Queries.GetEFileDocumentById;

namespace OfficeFlow.MVC.Controllers.EFiles
{
    public class EFilesDocumentsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EFilesDocumentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult Create(Guid publicId)
        {
            return View(new CreateEFilesDocumentsCommand(publicId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEFilesDocumentsCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction("Details", "EFiles", new { publicId = command.EFilePublicId });
        }

        [Route("EFiles/{publicId}/EFileDocuments/{id}/Details")]
        public async Task<IActionResult> Details(Guid publicId, int id)
        {
            var eFileDocument = await _mediator.Send(new GetEFileDocumentByIdQuery(id));

            return View(eFileDocument);
        }

        [Route("EFiles/{publicId}/EFileDocuments/{id}/Edit")]
        public async Task<IActionResult> Edit(Guid publicId, int id)
        {
            var eFileDocument = await _mediator.Send(new GetEFileDocumentByIdQuery(id));

            var model = _mapper.Map<EditEFilesDocumentsCommand>(eFileDocument);

            return View(model);
        }

        [HttpPost]
        [Route("EFiles/{publicId}/EFileDocuments/{id}/Edit")]
        public async Task<IActionResult> Edit(Guid publicId, int id, EditEFilesDocumentsCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction("Details", "EFiles", new { publicId = command.PublicId });
        }

        public async Task<IActionResult> DownloadFile(int id)
        {
            var document = await _mediator.Send(new GetDocumentByIdQuery(id));
            if (document == null || document.DocumentContent == null)
            {
                return NotFound();
            }

            return File(document.DocumentContent, document.DocumentContentType!, document.DocumentName);
        }
    }
}
