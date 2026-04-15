using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pustokApp.Data;
using pustokApp.Models;

namespace pustokApp.Areas.Manage.Controllers;

[Area("Manage")]
public class SliderController : Controller
{
    private readonly PustokAppDbContext _context;

    public SliderController(PustokAppDbContext context)
    {
        _context = context;
    }

    // GET: Manage/Slider/Index
    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.ToListAsync();
        return View(sliders);
    }

    // GET: Manage/Slider/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Manage/Slider/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,ImageUrl,ButtonText,ButtonUrl")] Slider slider)
    {
        if (ModelState.IsValid)
        {
            _context.Add(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(slider);
    }

    // GET: Manage/Slider/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slider = await _context.Sliders.FindAsync(id);
        if (slider == null)
        {
            return NotFound();
        }
        return View(slider);
    }

    // POST: Manage/Slider/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,ButtonText,ButtonUrl")] Slider slider)
    {
        if (id != slider.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(slider);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SliderExists(slider.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(slider);
    }

    // GET: Manage/Slider/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slider = await _context.Sliders
            .FirstOrDefaultAsync(m => m.Id == id);
        if (slider == null)
        {
            return NotFound();
        }

        return View(slider);
    }

    // POST: Manage/Slider/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var slider = await _context.Sliders.FindAsync(id);
        if (slider != null)
        {
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool SliderExists(int id)
    {
        return _context.Sliders.Any(e => e.Id == id);
    }
}
