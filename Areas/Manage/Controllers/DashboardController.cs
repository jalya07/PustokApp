using Microsoft.AspNetCore.Mvc;

namespace pustokApp.Areas.Manage.Controllers;
[Area("Manage")]
public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}