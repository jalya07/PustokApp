using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pustokApp.Models;
using pustokApp.ViewModels.UserVm;

namespace pustokApp.Controllers;

public class AccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    RoleManager<IdentityRole> roleManager
    ):Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        var user = await userManager.FindByNameAsync(vm.UserName);
        if (user != null)        {
            ModelState.AddModelError("UserName", "Username is already taken.");
            return View(vm);
        }
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

         user = new AppUser
        {
            UserName = vm.UserName,
            Email = vm.Email,
            FullName = vm.FullName
        };

        var result = await userManager.CreateAsync(user, vm.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(vm);
    }
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    // public async Task<IActionResult> CreateRole()
    // {
    //     await roleManager.CreateAsync(new IdentityRole("Admin"));
    //     await roleManager.CreateAsync(new IdentityRole("User"));
    //     await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
    //     return Content("Role created successfully");
    // }
}