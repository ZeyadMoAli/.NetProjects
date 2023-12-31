﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMoveisModels.Models;
using MyMoveisModels.Models.ViewModel;
using MyMoviesData.Repository.IRepository;

namespace MyMovies.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    // GET
    public IActionResult Index()
    {
        List<Product> objCategoryList = _unitOfWork.ProductRepository.GetAll( includeProperties: "category").ToList();

        return View(objCategoryList);
    }
    public IActionResult Upsert(int? id)
    {       
        
        ProductVM productVm = new()
        {
            CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u=>new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            } ),
            Product = new Product()
        };
        if (id == null || id == 0)
        {
            return View(productVm);
        }
        else
        {
            productVm.Product = _unitOfWork.ProductRepository.Get(u => u.ProductId == id);
            return View(productVm);
        }
         
    }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");
                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream =new FileStream(Path.Combine(productPath,fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\product\" + fileName;
            }

            if (productVM.Product.ProductId == 0)
            {
                _unitOfWork.ProductRepository.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.ProductRepository.Update(productVM.Product);
            }
            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        else
        {

            productVM.CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });

            return View(productVM);
        }
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? productFromDb = _unitOfWork.ProductRepository.Get(u => u.ProductId == id);
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb); 
    }
    [HttpPost,ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product obj =_unitOfWork.ProductRepository.Get(u => u.ProductId == id);
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.ProductRepository.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Product Deleted Successfully";
        return RedirectToAction("Index");
    }
    


}