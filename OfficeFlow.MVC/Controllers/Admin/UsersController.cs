using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.Users.Commands.CreateUsers;
using OfficeFlow.Application.Users.Commands.EditUsers;
using OfficeFlow.Application.Users.Models;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using OfficeFlow.Application.Users.Queries.GetUserByPublicId;

namespace OfficeFlow.MVC.Controllers.Admin;

public class UsersController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _mediator.Send(new GetAllUsersQuery());
        return View(list);
    }

    [Route("Users/{publicId}/Details")]
    public async Task<IActionResult> Details(Guid publicId)
    {
        var user = await _mediator.Send(new GetUserByPublicIdQuery(publicId));

        return View(user);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUsersCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }  

    [Route("Users/{publicId}/Edit")]
    public async Task<IActionResult> Edit(Guid publicId)
    {
        var user = await _mediator.Send(new GetUserByPublicIdQuery(publicId));

        var model = _mapper.Map<EditUsersCommand>(user);

        return View(model);
    }

    [HttpPost]
    [Route("Users/{publicId}/Edit")]
    public async Task<IActionResult> Edit(Guid publicId, EditUsersCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}
