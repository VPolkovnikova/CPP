using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Identity;
using WebApplication.Models.Account;

namespace WebApplication.Controllers;


public class AccountController : Controller
{
    public IActionResult Login(LoginModel.GetInput input) => User.Identity?.IsAuthenticated is true
        ? View()
        : Challenge(new AuthenticationProperties
        {
            RedirectUri = input.ReturnUrl
        });


    [Authorize]
    public IActionResult Logout(LogoutModel.GetInput input) => SignOut(
        new AuthenticationProperties
        {
            RedirectUri = input.ReturnUrl
        },
        CookieAuthenticationDefaults.AuthenticationScheme,
        OpenIdConnectDefaults.AuthenticationScheme
    );


    [Authorize]
    public IActionResult Information()
    {
        var user = User.GetUser();
        return View(new Information.Output()
        {
            UserName = user.UserName,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        });
    }
}