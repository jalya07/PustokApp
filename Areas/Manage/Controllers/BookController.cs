using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Models;

namespace pustokApp.Areas.Manage.Controllers;

[Area("Manage")]
public class BookController : Controller
{
    private readonly PustokAppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BookController(PustokAppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

// Затем в методе:

    // public BookController(PustokAppDbContext context)
    // {
    //     _context = context;
    // }

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
        ViewBag.Tags = _context.Tags.ToList();
        return View();
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Create([Bind("Id,Name,Desc,Price,Code,DiscountPercent,InStock,IsFeatured,IsNew,MainUrl,HoverUrl,AuthorId")] Book book)
    // {
    //     try
    //     {
    //         // Process file uploads BEFORE any validation
    //         if (Request.Form.Files.Count > 0)
    //         {
    //             // Get MainPhoto file (first file input)
    //             var mainPhotoFiles = Request.Form.Files.GetFiles("MainPhoto");
    //             if (mainPhotoFiles.Count > 0)
    //             {
    //                 var file = mainPhotoFiles[0];
    //                 if (file.Length > 0)
    //                 {
    //                     var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    //                     var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/books");
    //
    //                     if (!Directory.Exists(uploadsFolder))
    //                         Directory.CreateDirectory(uploadsFolder);
    //
    //                     var filePath = Path.Combine(uploadsFolder, fileName);
    //                     using (var fileStream = new FileStream(filePath, FileMode.Create))
    //                     {
    //                         await file.CopyToAsync(fileStream);
    //                     }
    //                     book.MainUrl = fileName;
    //                 }
    //             }
    //
    //             // Get HoverPhoto file (second file input)
    //             var hoverPhotoFiles = Request.Form.Files.GetFiles("HoverPhoto");
    //             if (hoverPhotoFiles.Count > 0)
    //             {
    //                 var file = hoverPhotoFiles[0];
    //                 if (file.Length > 0)
    //                 {
    //                     var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    //                     var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/books");
    //
    //                     if (!Directory.Exists(uploadsFolder))
    //                         Directory.CreateDirectory(uploadsFolder);
    //
    //                     var filePath = Path.Combine(uploadsFolder, fileName);
    //                     using (var fileStream = new FileStream(filePath, FileMode.Create))
    //                     {
    //                         await file.CopyToAsync(fileStream);
    //                     }
    //                     book.HoverUrl = fileName;
    //                 }
    //             }
    //         }
    //
    //         // Validate model (file properties are not bound, so no validation errors)
    //         if (!ModelState.IsValid)
    //         {
    //             ViewBag.Authors = _context.Authors.ToList();
    //             ViewBag.Tags = _context.Tags.ToList();
    //             return View(book);
    //         }
    //
    //         _context.Books.Add(book);
    //         await _context.SaveChangesAsync();
    //
    //         // Add tags to the book
    //         if (book.TagsId != null && book.TagsId.Count > 0)
    //         {
    //             foreach (var tagId in book.TagsId)
    //             {
    //                 var bookTag = new BookTag { BookId = book.Id, TagId = tagId };
    //                 _context.BookTags.Add(bookTag);
    //             }
    //             await _context.SaveChangesAsync();
    //         }
    //
    //         return RedirectToAction(nameof(Index));
    //     }
    //     catch (Exception ex)
    //     {
    //         ModelState.AddModelError("", "An error occurred while creating the book: " + ex.Message);
    //         ViewBag.Authors = _context.Authors.ToList();
    //         return View(book);
    //     }
    // }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        // Remove file properties from model state to prevent validation errors
        ModelState.Remove("Files");
        ModelState.Remove("MainPhoto");
        ModelState.Remove("HoverPhoto");

        if (!ModelState.IsValid)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View(book);
        }

        // Save MainPhoto
        if (book.MainPhoto != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(book.MainPhoto.FileName);
            var path = Path.Combine(_env.WebRootPath, "assets", "image", "books", fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await book.MainPhoto.CopyToAsync(stream);
            book.MainUrl = fileName;
        }

        // Save HoverPhoto
        if (book.HoverPhoto != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(book.HoverPhoto.FileName);
            var path = Path.Combine(_env.WebRootPath, "assets", "image", "books", fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await book.HoverPhoto.CopyToAsync(stream);
            book.HoverUrl = fileName;
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Add tags to the book
        if (book.TagsId != null && book.TagsId.Count > 0)
        {
            foreach (var tagId in book.TagsId)
            {
                var bookTag = new BookTag { BookId = book.Id, TagId = tagId };
                _context.BookTags.Add(bookTag);
            }
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }


// GET: Manage/Book/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.Include(b => b.BookTags).FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
            return NotFound();

        ViewBag.Authors = _context.Authors.ToList();
        ViewBag.Tags = _context.Tags.ToList();
        
        // Get currently selected tag IDs
        book.TagsId = book.BookTags?.Select(bt => bt.TagId).ToList() ?? new List<int>();
        
        return View(book);
    }

    // POST: Manage/Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id)
            return NotFound();

        // Remove file properties from model state to prevent validation errors
        ModelState.Remove("Files");
        ModelState.Remove("MainPhoto");
        ModelState.Remove("HoverPhoto");

        // Get existing book to preserve images if not changed
        var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (existingBook == null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View(book);
        }

        // Handle MainPhoto
        if (book.MainPhoto != null)
        {
            var file = book.MainPhoto;
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(_env.WebRootPath, "assets", "image", "books", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                book.MainUrl = fileName;
            }
            else
            {
                book.MainUrl = existingBook.MainUrl;
            }
        }
        else
        {
            book.MainUrl = existingBook.MainUrl;
        }

        // Handle HoverPhoto
        if (book.HoverPhoto != null)
        {
            var file = book.HoverPhoto;
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(_env.WebRootPath, "assets", "image", "books", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                book.HoverUrl = fileName;
            }
            else
            {
                book.HoverUrl = existingBook.HoverUrl;
            }
        }
        else
        {
            book.HoverUrl = existingBook.HoverUrl;
        }

        _context.Update(book);
        await _context.SaveChangesAsync();

        // Update tags
        var existingTags = _context.BookTags.Where(bt => bt.BookId == id).ToList();
        _context.BookTags.RemoveRange(existingTags);

        // Add new tags
        if (book.TagsId != null && book.TagsId.Count > 0)
        {
            foreach (var tagId in book.TagsId)
            {
                var bookTag = new BookTag { BookId = book.Id, TagId = tagId };
                _context.BookTags.Add(bookTag);
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
