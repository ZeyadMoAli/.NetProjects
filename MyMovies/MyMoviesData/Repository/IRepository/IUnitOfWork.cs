namespace MyMoviesData.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository categoryRepository { get; }
    IProductRepository ProductRepository { get; }
    void Save();
}