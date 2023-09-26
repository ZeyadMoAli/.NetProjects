using DataAccess.Data;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity.Controllers;

public class AuthorController : Controller
{
    private readonly ApplicationDbContext _db;

    public AuthorController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Author> authors = _db.Authors.ToList();
        return View(authors);
    }

    public IActionResult Upsert(int? id)
    {
        Author obj = new();
        if (id == null || id == 0)
        {
            return View(obj);
        }

        obj = _db.Authors.FirstOrDefault(u=>u.AuthorId==id);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Author obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.AuthorId == 0)
            {
                await _db.Authors.AddAsync(obj);
            }
            else
            {
                _db.Authors.Update(obj);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(obj);

    }
    public async Task<IActionResult> Delete(int? id)
    {
        Author obj = new();
        obj = _db.Authors.FirstOrDefault(u=>u.AuthorId==id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Authors.Remove(obj);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }
}