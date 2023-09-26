using DataAccess.Data;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity.Controllers;

public class PublisherController : Controller
{
    private readonly ApplicationDbContext _db;

    public PublisherController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        List<Publisher> publishers = _db.Publishers.ToList();
        return View(publishers);
    }

    public IActionResult Upsert(int? id)
    {
        Publisher obj = new();
        if (id == null || id == 0)
        {
            return View(obj);
        }

        obj = _db.Publishers.FirstOrDefault(u=>u.PublisherId==id);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Publisher obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.PublisherId == 0)
            {
                await _db.Publishers.AddAsync(obj);
            }
            else
            {
                _db.Publishers.Update(obj);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(obj);

    }
    public async Task<IActionResult> Delete(int? id)
    {
        Publisher obj = new();
        obj = _db.Publishers.FirstOrDefault(u=>u.PublisherId==id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Publishers.Remove(obj);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}