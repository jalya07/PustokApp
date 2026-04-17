using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using pustokApp.Service;
using pustokApp.Setting;

namespace pustokApp.Controllers;

public class BasketController() : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult SetCookie(string key, string value)
    {
        Response.Cookies.Append("pustokCookie", "pustokk");
        return Content("Cookie set successfully");
    }
    public IActionResult SetSession(string key, string value)
    {
        HttpContext.Session.SetString("pustokSession", "pustokk");
        return Content("Session set successfully");
    }
    public IActionResult GetSession(string key)
    {
        var sessionValue = HttpContext.Session.GetString("pustokSession");
        return Content($"Session value: {sessionValue}");
    }
}