using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using IdentityServer.Data.Entities;
using IdentityServer.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;


public class AccountController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IIdentityServerInteractionService interactionService
) : Controller
{
    public IActionResult Register() => User.IsAuthenticated()
        ? RegisterIfAuthenticated()
        : View(new RegisterModel.Output());


    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel.PostInput input)
    {
        if (User.IsAuthenticated())
        {
            return RegisterIfAuthenticated();
        }
        if (!ModelState.IsValid)
        {
            return View(input.ToOutput());
        }

        var result = await userManager.CreateAsync(
            new User
            {
                UserName = input.UserName,
                FullName = input.FullName,
                PhoneNumber = input.PhoneNumber,
                Email = input.Email
            },
            input.Password
        );
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        foreach (var error in result.Errors)
        {
            ModelState.TryAddModelError("", error.Description);
        }
        return View(input.ToOutput());
    }


    public IActionResult Login(LoginModel.GetInput input) => User.IsAuthenticated()
        ? LoginIfAuthenticated(input.ReturnUrl)
        : View(input.ToOutput());


    [HttpPost]
    public async Task<IActionResult> Login(LoginModel.PostInput input)
    {
        if (User.IsAuthenticated())
        {
            return LoginIfAuthenticated(input.ReturnUrl);
        }
        if (!ModelState.IsValid)
        {
            return View(input.ToOutput());
        }

        var result = await signInManager.PasswordSignInAsync(
            input.UserName,
            input.Password,
            input.IsPersistent,
            lockoutOnFailure: false
        );
        if (result.Succeeded)
        {
            return input.ReturnUrl is null
                ? RedirectToAction("Index", "Home")
                : Redirect(input.ReturnUrl);
        }
        ModelState.AddModelError("", "Name or password is incorrect.");
        return View(input.ToOutput());
    }


    [Authorize]
    public async Task<IActionResult> Logout(LogoutModel.GetInput input)
    {
        var context = await interactionService.GetLogoutContextAsync(input.LogoutId);
        await signInManager.SignOutAsync();
        return context?.PostLogoutRedirectUri is null
            ? RedirectToAction("Index", "Home")
            : Redirect(context.PostLogoutRedirectUri);
    }


    private ViewResult RegisterIfAuthenticated() => View(new RegisterModel.Output());


    private IActionResult LoginIfAuthenticated(string? returnUrl) => returnUrl is null
        ? View(new LoginModel.Output())
        : RedirectToAction(returnUrl);
}