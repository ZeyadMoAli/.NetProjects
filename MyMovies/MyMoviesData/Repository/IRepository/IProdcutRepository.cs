using MyMoveisModels.Models;

namespace MyMoviesData.Repository.IRepository;

public interface IProductRepository: IRepository<Product>
{
    void Update(Product product);

}