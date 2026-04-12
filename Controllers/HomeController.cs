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
            Sliders = context.Sliders.ToList(),
            FeaturedBooks = context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.IsFeatured)
                .ToList(),
            NewBooks = context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.IsNew)
                .ToList(),
            DiscountedBooks = context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.DiscountPercent > 0)
                .ToList()

        };
        return View(homeVm);
    }
}