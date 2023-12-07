using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.Users.Models;
using System.Security.Claims;
using MediatR;
using OfficeFlow.Application.Users.Queries.LogInUser;

namespace OfficeFlow.MVC.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                View(model);
            }

            var user = await _mediator.Send(new LogInUserQuery(model));

            if (user != null)
            {
                // Utwórz listę deklaracji tożsamości (claims)
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                        new Claim(ClaimTypes.Role, user.Role)
                        // Dodaj więcej claims, jeśli to konieczne
                    };

                // Utwórz tożsamość na podstawie listy claims
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Utwórz właściwości autentykacji
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Jeśli chcesz, aby sesja była trwała
                };

                // Zaloguj użytkownika
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
