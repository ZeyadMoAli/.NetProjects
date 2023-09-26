using DataAccess.Data;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity.Controllers;

public class BookController : Controller
{
    private readonly ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Book> books = _db.Books.ToList();
        return View(books);
    }

    public IActionResult Upsert(int? id)
    {
        Category obj = new();
        if (id == null || id == 0)
        {
            return View(obj);
        }

        obj = _db.Categories.FirstOrDefault(u=>u.CategoryId==id);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Category obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.CategoryId == 0)
            {
                await _db.Categories.AddAsync(obj);
            }
            else
            {
                _db.Categories.Update(obj);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(obj);

    }
    public async Task<IActionResult> Delete(int? id)
    {
        Category obj = new();
        obj = _db.Categories.FirstOrDefault(u=>u.CategoryId==id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(obj);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }

}