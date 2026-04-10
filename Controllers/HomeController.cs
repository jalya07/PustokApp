using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pustokApp.Models;

namespace pustokApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
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