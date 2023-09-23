using MyMoveisModels.Models;
using MyMoviesData.Data;
using MyMoviesData.Repository.IRepository;

namespace MyMoviesData.Repository;

public class CategoryRepository: Repository<Category>,ICategoryRepository
{
    private ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;

    }

    public void Update(Category category)
    {
        _db.Category.Update(category);
    }

  
}