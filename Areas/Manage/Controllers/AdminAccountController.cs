using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using pustokApp.Areas.Manage.ViewModels;
using pustokApp.Models;

namespace pustokApp.Areas.Manage.Controllers;

[Area("Manage")]
public class AdminAccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager
     ) : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> CreateAdmin()
    {
        var admin = new AppUser
        {
            UserName = "admin",
            Email = "admin@pustok.com",
            FullName = "Admin User",
            IsAdmin = true,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, "Admin123!@");

        if (result.Succeeded)
        {
            return Content("Admin user created successfully!", "text/plain");
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Json(result.Errors);
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AdminLoginVm loginVm)
    {
        if (!ModelState.IsValid)
            return View(loginVm);
        var user = await userManager.FindByNameAsync(loginVm.Username);
        if (user != null && await userManager.CheckPasswordAsync(user, loginVm.Password))
        {
            // Sign in the user
            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsAdmin", "true"));
            await signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Dashboard");
        }
        ModelState.AddModelError("", "Invalid username or password");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}


