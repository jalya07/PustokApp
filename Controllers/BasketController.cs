using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using pustokApp.Service;
using pustokApp.Setting;

namespace pustokApp.Controllers;

public class BasketController(
    IConfiguration config,
    BankService bankService,
    BankManager bankManager,
    IOptions<GroupInfoSettings> groupInfoSettings 
    ) : Controller
{
    public IActionResult Index()
    {
        bankService.Add();
        bankManager.Add();
        
        return Content(""+bankService.Balance);
    }

    public IActionResult ShowInfo()
    {
        var groupInfo = groupInfoSettings.Value;
        var stuName = groupInfo.StudentName;
        var stuSurname = groupInfo.StudentSurname;
        return Content($"Student name: {stuName} Student surname: {stuSurname}");
    }
}