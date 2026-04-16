using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Models;

namespace pustokApp.Areas.Manage.Controllers;

[Area("Manage")]
public class BookController : Controller
{
    private readonly PustokAppDbContext _context;

    public BookController(PustokAppDbContext context)
    {
        _context = context;
    }

    // GET: Manage/Book/Index
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookImages)
            .Include(b => b.BookTags)
            .ToListAsync();
        return View(books);
    }

    public IActionResult Create()
    {
        ViewBag.Authors = _context.Authors.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        try
        {
            // Remove file properties from model state to avoid validation errors
            ModelState.Remove("MainPhoto");
            ModelState.Remove("HoverPhoto");
            ModelState.Remove("Files");

            // Handle Main Image Upload from MainPhoto property
            if (book.MainPhoto != null && book.MainPhoto.Count > 0)
            {
                var file = book.MainPhoto[0];
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/books");
                    
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    book.MainUrl = fileName;
                }
            }

            // Handle Hover Image Upload from HoverPhoto property
            if (book.HoverPhoto != null && book.HoverPhoto.Count > 0)
            {
                var file = book.HoverPhoto[0];
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/books");
                    
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    book.HoverUrl = fileName;
                }
            }

            // Validate model
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                return View(book);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while creating the book: " + ex.Message);
            ViewBag.Authors = _context.Authors.ToList();
            return View(book);
        }
    }
}
