using MyMoveisModels.Models;

namespace MyMoviesData.Repository.IRepository;

public interface ICategoryRepository: IRepository<Category>
{
    void Update(Category category);

}