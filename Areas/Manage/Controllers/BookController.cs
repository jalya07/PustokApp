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
    public async Task<IActionResult> Create([Bind("Id,Name,Desc,Price,Code,DiscountPercent,InStock,IsFeatured,IsNew,MainUrl,HoverUrl,AuthorId")] Book book)
    {
        try
        {
            // Process file uploads BEFORE any validation
            if (Request.Form.Files.Count > 0)
            {
                // Get MainPhoto file (first file input)
                var mainPhotoFiles = Request.Form.Files.GetFiles("MainPhoto");
                if (mainPhotoFiles.Count > 0)
                {
                    var file = mainPhotoFiles[0];
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

                // Get HoverPhoto file (second file input)
                var hoverPhotoFiles = Request.Form.Files.GetFiles("HoverPhoto");
                if (hoverPhotoFiles.Count > 0)
                {
                    var file = hoverPhotoFiles[0];
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
            }

            // Validate model (file properties are not bound, so no validation errors)
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

    // GET: Manage/Book/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        ViewBag.Authors = _context.Authors.ToList();
        return View(book);
    }

    // POST: Manage/Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Desc,Price,Code,DiscountPercent,InStock,IsFeatured,IsNew,MainUrl,HoverUrl,AuthorId")] Book book)
    {
        if (id != book.Id)
            return NotFound();

        try
        {
            // Process file uploads if provided
            if (Request.Form.Files.Count > 0)
            {
                // Get MainPhoto file
                var mainPhotoFiles = Request.Form.Files.GetFiles("MainPhoto");
                if (mainPhotoFiles.Count > 0)
                {
                    var file = mainPhotoFiles[0];
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

                // Get HoverPhoto file
                var hoverPhotoFiles = Request.Form.Files.GetFiles("HoverPhoto");
                if (hoverPhotoFiles.Count > 0)
                {
                    var file = hoverPhotoFiles[0];
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
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                return View(book);
            }

            _context.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while updating the book: " + ex.Message);
            ViewBag.Authors = _context.Authors.ToList();
            return View(book);
        }
    }

    // GET: Manage/Book/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
