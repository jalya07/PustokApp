using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Models;
using pustokApp.ViewModels;

namespace pustokApp.Controllers;

public class BooksController(PustokAppDbContext context): Controller
{
    public IActionResult Details(int id)
    {
        var book = context.Books
            .Include(x=>x.Author)
            .Include(x=>x.BookImages)
            .Include(x=>x.BookTags)
            .ThenInclude(x=>x.Tag)
            .FirstOrDefault(b=> b.Id == id);
        BookVm bookVm = new BookVm
        {
            Book = book,
            RelatedBooks = context.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.AuthorId == book.AuthorId && x.Id != book.Id)
                .Take(4)
                .ToList()
        };
        return View(bookVm);
    }

    public IActionResult BookModal(int id)
    {
        var book = context.Books
            .Include(x => x.Author)
            .Include(x => x.BookImages)
            .Include(x => x.BookTags)
            .ThenInclude(x => x.Tag)
            .FirstOrDefault(b => b.Id == id);
        if (book==null)
            return NotFound();
        
        return PartialView("_BookModalPartialView", book);
    }
}