using Microsoft.AspNetCore.Mvc;
using pustokApp.Service;

namespace pustokApp.Controllers;

public class BasketController(
    IConfiguration config,
    BankService bankService,
    BankManager bankManager) : Controller
{
    public IActionResult Index()
    {
        bankService.Add();
        bankManager.Add();
        
        return Content(""+bankService.Balance);
    }

    public IActionResult ShowInfo()
    {
        var stuName = config.GetSection("GroupInfo:StudentName").Value;
        var stuSurname = config.GetSection("GroupInfo:StudentSurname").Value;
        return Content($"Student name: {stuName} Student surname: {stuSurname}");
    }
}