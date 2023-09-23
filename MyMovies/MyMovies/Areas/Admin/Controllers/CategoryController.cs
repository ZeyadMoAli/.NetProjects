using Microsoft.AspNetCore.Mvc;
using MyMoveisModels.Models;
using MyMoviesData.Data;
using MyMoviesData.Repository.IRepository;

namespace MyMovies.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET
    public IActionResult Index()
    {
        List<Category> ObjCategoryList = _unitOfWork.categoryRepository.GetAll().ToList();
        return View(ObjCategoryList);
    }
    public IActionResult Create()
    {
        return View(); 
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name","The Display Order cannot exactly match the Name");
        }
        if (obj.Name.ToLower()=="test")
        {
            ModelState.AddModelError("","test is an invalid value");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.categoryRepository.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }

        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category categoryFromDb =_unitOfWork.categoryRepository.Get(u => u.CategoryId == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb); 
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.categoryRepository.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromDb = _unitOfWork.categoryRepository.Get(u => u.CategoryId == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb); 
    }

    [HttpPost,ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category obj =_unitOfWork.categoryRepository.Get(u => u.CategoryId == id);
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.categoryRepository.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");
    }
}