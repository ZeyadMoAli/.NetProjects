using MyMoveisModels.Models;
using MyMoviesData.Data;
using MyMoviesData.Repository.IRepository;

namespace MyMoviesData.Repository;

public class ProductRepository: Repository<Product>,IProductRepository
{
    private ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;

    }

    public void Update(Product product)
    {
        var objFromDb= _db.Products.FirstOrDefault(u => u.ProductId ==product.ProductId);
        if (objFromDb != null)
        {
            objFromDb.Title = product.Title;
            objFromDb.ISBN = product.ISBN;
            objFromDb.Price = product.Price;
            objFromDb.Price50 = product.Price50;
            objFromDb.ListPrice = product.ListPrice;
            objFromDb.Price100 = product.Price100;
            objFromDb.Description = product.Description;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.Autor = product.Autor;
            if (product.ImageUrl != null)
            {
                objFromDb.ImageUrl = product.ImageUrl; 
            }
        }
    }

  
}