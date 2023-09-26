using DataAccess.Data;
using EntityFrameWorkPractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelss.ViewModels;

namespace EntityFrameWorkPractice.Controllers;

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
        List<Book> books = _db.Books.Include(u=>u.Publisher).ToList();
        return View(books);
    }
    public IActionResult Upsert(int? id)
    {
        BookVm obj = new();
        obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.PublisherId.ToString()
        });
        if (id == null || id == 0)
        {
            return View(obj);
        }

        obj.Book = _db.Books.FirstOrDefault(u=>u.Id==id);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(BookVm obj)
    {
        
            if (obj.Book.Id == 0)
            {
                await _db.Books.AddAsync(obj.Book);
            }
            else
            {
                _db.Books.Update(obj.Book);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
    }
    public async Task<IActionResult> Delete(int? id)
    {
        Book obj = new();
        obj = _db.Books.FirstOrDefault(u=>u.Id==id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Books.Remove(obj);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }
    public IActionResult Details(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        BookDetail obj = new();

        obj = _db.BookDetails.Include(u=>u.Book).FirstOrDefault(u => u.BookId == id);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(BookDetail obj)
    {
        if (obj.BookDetailID == 0)
        {
            await _db.BookDetails.AddAsync(obj);
        }
        else
        {
            _db.BookDetails.Update(obj);
        }
            
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
        
    }
    public async Task<IActionResult> Playground(int? id)
    {
        var bookTemp = _db.Books.FirstOrDefault();
        bookTemp.Price = 100;

        var bookCollection = _db.Books;
        double totalPrice = 0;

        foreach(var book in bookCollection)
        {
            totalPrice += book.Price;
        }

        var bookList = _db.Books.ToList();
        foreach (var book in bookList)
        {
            totalPrice += book.Price;
        }

        var bookCollection2 = _db.Books;
        var bookCount1 = bookCollection2.Count();

        var bookCount2 = _db.Books.Count();

        return RedirectToAction(nameof(Index));

    }
}