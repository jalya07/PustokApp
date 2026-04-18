using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = await userManager.FindByNameAsync(vm.UserNameOrEmail);
        if (user == null)
        {
            user = await userManager.FindByEmailAsync(vm.UserNameOrEmail);
        }

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or email.");
            return View(vm);
        }

        if (await userManager.IsInRoleAsync(user, "Admin"))
        {
            ModelState.AddModelError(string.Empty, "Admin users cannot log in here.");
            return View(vm);
        }
        
        var result = await signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Account is locked out.");
            return View(vm);
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(vm);
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
    
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserProfile()
    {
        var user = await userManager.GetUserAsync(User);
        UserProfileVm userProfileVm = new UserProfileVm
        {
            UserInfo = new UserProfileInfoVm
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email
            }
        };
        return View(userProfileVm);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> UserProfile(UserProfileVm vm)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!ModelState.IsValid)
        {
            return View(vm);
        }
       
        // Update basic info
        user.FullName = vm.UserInfo.FullName;
        user.UserName = vm.UserInfo.UserName;
        user.Email = vm.UserInfo.Email;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(vm);
        }

        // Handle password change if provided
        if (!string.IsNullOrEmpty(vm.UserInfo.NewPassword))
        {
            if (vm.UserInfo.NewPassword != vm.UserInfo.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "New password and confirm password do not match.");
                return View(vm);
            }

            var passwordResult = await userManager.ChangePasswordAsync(user, vm.UserInfo.CurrentPassword, vm.UserInfo.NewPassword);

            if (!passwordResult.Succeeded)
            {
                foreach (var error in passwordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(vm);
            }
        }
        await signInManager.SignOutAsync();
        TempData["Success"] = "Your profile has been updated successfully.";
        return RedirectToAction("UserProfile");
    }
    
    // public async Task<IActionResult> CreateRole()
    // {
    //     await roleManager.CreateAsync(new IdentityRole("Admin"));
    //     await roleManager.CreateAsync(new IdentityRole("User"));
    //     await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
    //     return Content("Role created successfully");
    // }
}