using MyMoveisModels.Models;
using MyMoviesData.Data;
using MyMoviesData.Repository.IRepository;

namespace MyMoviesData.Repository;

public class UnitOfWork:IUnitOfWork
{
    private ApplicationDbContext _db;
    public ICategoryRepository categoryRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext db) 
    {
        _db = db;
        categoryRepository = new CategoryRepository(_db);
        ProductRepository = new ProductRepository(_db); 

    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}