using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using pustokApp.Data;
using pustokApp.Models;
using pustokApp.Service;
using pustokApp.Setting;
using pustokApp.ViewModels;

namespace pustokApp.Controllers;

public class BasketController(PustokAppDbContext context) : Controller
{
    public IActionResult AddBasket(int id)
    {
        var book=context.Books.Find(id);
        if (book == null)
            return NotFound();
        List<BasketItemVm> basketItems;
        var basketStr=Request.Cookies["pustokSession"];
        if (string.IsNullOrEmpty(basketStr))
        {
            basketItems=new List<BasketItemVm>();
        }
        else
        {
            basketItems=Newtonsoft.Json.JsonConvert.DeserializeObject<List<BasketItemVm>>(basketStr);
        }
        var existbasketItem=basketItems.FirstOrDefault(b => b.BookId == id);
        if (existbasketItem == null)     
        {
            basketItems.Add(new BasketItemVm
            {
                BookId = book.Id,
                BookName = book.Name,
                BookPrice = (decimal)(book.DiscountPercent > 0 ?book.Price - (book.DiscountPercent*book.Price/100):book.Price),
                Count = 1,
                MainImageUrl = book.MainUrl
            });
        }
        else        {
            existbasketItem.Count++;
        }

        if (User.Identity.IsAuthenticated)
        {
         // var user=context.Users
             // .Include(u => u.BasketItems)
             // .FirstOrDefault(u => u.UserName == User.Identity.Name);
        }
        Response.Cookies.Append("pustokSession", Newtonsoft.Json.JsonConvert.SerializeObject(basketItems));
            
        
        return PartialView("_BasketPartial",new List<BasketItem>());
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