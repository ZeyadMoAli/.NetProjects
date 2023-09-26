using DataAccess.Data;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Category> categories = _db.Categories.ToList();
        return View(categories);
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

    public IActionResult CreateMultiple2()
    {
        for (int i = 1; i <= 2; i++)
        {
            _db.Categories.Add(new Category { GenreName = Guid.NewGuid().ToString() });
        }

        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult CreateMultiple5()
    {
        for (int i = 1; i <= 5; i++)
        {
            _db.Categories.Add(new Category { GenreName = Guid.NewGuid().ToString() });
        }

        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult RemoveMultiple2()
    {
        List<Category> categories = _db.Categories.OrderByDescending(u => u.CategoryId).Take(2).ToList();
        _db.Categories.RemoveRange(categories);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult RemoveMultiple5()
    {
        List<Category> categories = _db.Categories.OrderByDescending(u => u.CategoryId).Take(5).ToList();
        _db.Categories.RemoveRange(categories);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    
}