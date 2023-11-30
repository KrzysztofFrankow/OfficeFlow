using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.EFilesDocuments.Commands.CreateEFilesDocuments;

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
    }
}
