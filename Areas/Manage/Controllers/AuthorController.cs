using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Models;

namespace pustokApp.Areas.Manage.Controllers;

[Area("Manage")]
public class AuthorController : Controller
{
    private readonly PustokAppDbContext _context;

    public AuthorController(PustokAppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _context.Authors.ToListAsync();
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid)
            return View(author);
        Author newAuthor = new Author()
        {
            FullName = author.FullName,
            Id = author.Id
        };
        _context.Authors.Add(newAuthor);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        if (author == null)
            return NotFound();

        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Author author)
    {
        if (id != author.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Authors.Update(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Authors.AsEnumerable().Any(a => a.Id == author.Id))
                    return NotFound();
                throw;
            }
        }
        return View(author);
    }
    
    // GET - Delete Author
    public IActionResult Delete(int id)
    {
        var author = _context.Authors.Find(id);
        if (author == null)
            return NotFound();

        _context.Authors.Remove(author);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    // GET - Full Details Page
    public IActionResult Details(int id)
    {
        var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        if (author == null)
            return NotFound();

        return View("Detail", author);
    }

    // GET - Partial View for Modal
    public IActionResult GetAuthorDetailsPartial(int id)
    {
        var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        if (author == null)
            return NotFound();

        return PartialView("_AuthorDetailsPartial", author);
    }
}