using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Absences.Commands.DeleteAbsence;
using OfficeFlow.Application.Dictionaries.Queries.GetEFileCategoryDictionaries;
using OfficeFlow.Application.Dictionaries.Queries.GetEFileTypeDictionaries;
using OfficeFlow.Application.EFilesDocuments.Commands.CreateEFilesDocuments;
using OfficeFlow.Application.EFilesDocuments.Commands.DeleteEfileDocuments;
using OfficeFlow.Application.EFilesDocuments.Commands.EditEFilesDocuments;
using OfficeFlow.Application.EFilesDocuments.Queries.GetDocumentById;
using OfficeFlow.Application.EFilesDocuments.Queries.GetEFileDocumentById;
using OfficeFlow.Application.Roles.Queries.GetAllRoles;

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

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Create(Guid publicId)
        {
            var categories = await _mediator.Send(new GetEFileCategoryDictionariesQuery());
            ViewBag.Categories = new SelectList(categories, "Value", "Text");

            var types = await _mediator.Send(new GetEFileTypeDictionariesQuery());
            ViewBag.Types = new SelectList(types, "Value", "Text");

            return View(new CreateEFilesDocumentsCommand(publicId));
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Create(CreateEFilesDocumentsCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction("Details", "EFiles", new { publicId = command.EFilePublicId });
        }

        [Authorize(Roles = "Manager, Admin")]
        [Route("EFiles/{publicId}/EFileDocuments/{id}/Details")]
        public async Task<IActionResult> Details(Guid publicId, int id)
        {
            var eFileDocument = await _mediator.Send(new GetEFileDocumentByIdQuery(id));

            return View(eFileDocument);
        }

        [Authorize(Roles = "Manager, Admin")]
        [Route("EFiles/{publicId}/EFileDocuments/{id}/Edit")]
        public async Task<IActionResult> Edit(Guid publicId, int id)
        {
            var categories = await _mediator.Send(new GetEFileCategoryDictionariesQuery());
            ViewBag.Categories = new SelectList(categories, "Value", "Text");

            var types = await _mediator.Send(new GetEFileTypeDictionariesQuery());
            ViewBag.Types = new SelectList(types, "Value", "Text");

            var eFileDocument = await _mediator.Send(new GetEFileDocumentByIdQuery(id));

            var model = _mapper.Map<EditEFilesDocumentsCommand>(eFileDocument);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Admin")]
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

        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var document = await _mediator.Send(new GetDocumentByIdQuery(id));
            if (document == null || document.DocumentContent == null)
            {
                return NotFound();
            }

            return File(document.DocumentContent, document.DocumentContentType!, document.DocumentName);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteEFileDocumentsCommand(id));

            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

            return RedirectToAction("Index", "Absences");
        }
    }
}
