﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Roles.Queries.GetAllRoles;
using OfficeFlow.Application.Users.Commands.CreateUsers;
using OfficeFlow.Application.Users.Commands.DeleteUsers;
using OfficeFlow.Application.Users.Commands.EditUsers;
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var list = await _mediator.Send(new GetAllUsersQuery());
        return View(list);
    }

    [Authorize(Roles = "Admin")]
    [Route("Users/{publicId}/Details")]
    public async Task<IActionResult> Details(Guid publicId)
    {
        var user = await _mediator.Send(new GetUserByPublicIdQuery(publicId));

        return View(user);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var roles = await _mediator.Send(new GetAllRolesQuery());

        var roleSelectListItems = roles.Select(role => new SelectListItem
        {
            Text = role.Name,
            Value = role.Id.ToString()
        })
        .ToList();

        roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz rolę...", Value = "" });
        ViewBag.Roles = new SelectList(roleSelectListItems, "Value", "Text");

        return View();
    }


    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [Route("Users/{publicId}/Edit")]
    public async Task<IActionResult> Edit(Guid publicId)
    {
        var user = await _mediator.Send(new GetUserByPublicIdQuery(publicId));

        var model = _mapper.Map<EditUsersCommand>(user);

        var roles = await _mediator.Send(new GetAllRolesQuery());

        var roleSelectListItems = roles.Select(role => new SelectListItem
        {
            Text = role.Name,
            Value = role.Id.ToString()
        })
        .ToList();

        roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz rolę...", Value = "" });
        ViewBag.Roles = new SelectList(roleSelectListItems, "Value", "Text");

        return View(model);
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid publicId)
    {
        await _mediator.Send(new DeleteUsersCommand(publicId));

        var referer = Request.Headers["Referer"].ToString();

        if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

        return RedirectToAction("Index", "Absences");
    }
}
