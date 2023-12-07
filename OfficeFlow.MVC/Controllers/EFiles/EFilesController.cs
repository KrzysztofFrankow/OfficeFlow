using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.EFiles.Commands.CreateEFiles;
using OfficeFlow.Application.EFiles.Queries.GetAllEFiles;
using OfficeFlow.Application.EFiles.Queries.GetEFileByPublicId;
using OfficeFlow.Application.EFiles.Commands.EditEFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Roles.Queries.GetAllRoles;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using OfficeFlow.Application.EFilesDocuments.Commands.DeleteEfileDocuments;
using OfficeFlow.Application.EFiles.Commands.DeleteEFiles;

namespace OfficeFlow.MVC.Controllers.EFiles;

public class EFilesController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EFilesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Create()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());

        var roleSelectListItems = users.Select(user => new SelectListItem
        {
            Text = user.FirstName + " " + user.LastName,
            Value = user.Id.ToString()
        })
        .ToList();

        roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz pracownika...", Value = "" });
        ViewBag.Users = new SelectList(roleSelectListItems, "Value", "Text");

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Create(CreateEFilesCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Index()
    {
        var list = await _mediator.Send(new GetAllEFilesQuery());
        return View(list);
    }

    [Authorize(Roles = "Manager, Admin")]
    [Route("EFiles/{publicId}/Details")]
    public async Task<IActionResult> Details(Guid publicId)
    {
        var eFile = await _mediator.Send(new GetEFileByPublicIdQuery(publicId));

        return View(eFile);
    }

    [Authorize(Roles = "Manager, Admin")]
    [Route("EFiles/{publicId}/Edit")]
    public async Task<IActionResult> Edit(Guid publicId)
    {
        var users = await _mediator.Send(new GetAllUsersQuery());

        var roleSelectListItems = users.Select(user => new SelectListItem
        {
            Text = user.FirstName + " " + user.LastName,
            Value = user.Id.ToString()
        })
        .ToList();

        roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz użytkownika...", Value = "" });
        ViewBag.Users = new SelectList(roleSelectListItems, "Value", "Text");

        var eFile = await _mediator.Send(new GetEFileByPublicIdQuery(publicId));

        var model = _mapper.Map<EditEFilesCommand>(eFile);

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Manager, Admin")]
    [Route("EFiles/{publicId}/Edit")]
    public async Task<IActionResult> Edit(Guid publicId, EditEFilesCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Delete(Guid publicId)
    {
        await _mediator.Send(new DeleteEFilesCommand(publicId));

        var referer = Request.Headers["Referer"].ToString();

        if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

        return RedirectToAction("Index", "Absences");
    }
}
