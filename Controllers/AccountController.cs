﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using PresentroMVC.Models;
using PresentroMVC.ViewModels.AccountVM;

namespace PresentroMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        AppUser newUser = new AppUser()
        {
            Name = registerVM.Name,
            Surname = registerVM.Surname,
            Email = registerVM.Email,
            UserName = registerVM.UserName,
        };
        IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError? item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> LogIn(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

        if (appUser is null)
        {
            ModelState.AddModelError("", "Email or Passwor Wrong");
            return View();
        }
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, false);
        if(!result.Succeeded)
        {
            ModelState.AddModelError("", "Email or Passwor Wrong");
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
