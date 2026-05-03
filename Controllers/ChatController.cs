using Microsoft.AspNetCore.Mvc;

namespace pustokApp.Controllers;

public class ChatController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}