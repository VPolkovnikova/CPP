﻿using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;


public class HomeController : Controller
{
    public IActionResult Index() => View();


    public IActionResult Error() => View();
}