using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pustokApp.Models;
using pustokApp.Data;
using Microsoft.EntityFrameworkCore;
using pustokApp.ViewModels;

namespace pustokApp.Controllers;

public class HomeController(PustokAppDbContext context) : Controller
{
    private readonly PustokAppDbContext _context = context;
    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.ToListAsync();
        HomeVm homeVm = new HomeVm
        {
            Sliders = sliders
        };
        return View(homeVm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Subscribe(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Index");
        }
        
        // TODO: Implementiraj logiku za newsletter subscription
        // Npr. spremi email u bazu podataka ili pošalji email
        
        TempData["SuccessMessage"] = "Hvala na prijavi za newsletter!";
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}