using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.EFiles.Commands.CreateEFiles;
using OfficeFlow.Application.EFiles.Queries.GetAllEFiles;
using OfficeFlow.Application.EFiles.Queries.GetEFileByPublicId;
using OfficeFlow.Application.EFiles.Commands.EditEFiles;

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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEFilesCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Index()
    {
        var list = await _mediator.Send(new GetAllEFilesQuery());
        return View(list);
    }

    [Route("EFiles/{publicId}/Details")]
    public async Task<IActionResult> Details(Guid publicId)
    {
        var user = await _mediator.Send(new GetEFileByPublicIdQuery(publicId));

        return View(user);
    }

    [Route("EFiles/{publicId}/Edit")]
    public async Task<IActionResult> Edit(Guid publicId)
    {
        var user = await _mediator.Send(new GetEFileByPublicIdQuery(publicId));

        var model = _mapper.Map<EditEFilesCommand>(user);

        return View(model);
    }

    [HttpPost]
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
}
