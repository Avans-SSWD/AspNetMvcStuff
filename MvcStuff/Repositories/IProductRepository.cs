using MvcStuff.Domain.Models;

namespace MvcStuff.Repositories;
public interface IProductRepository
{
    void Add(Product product);
    void Delete(int id);
    Product? Get(int id);
    IEnumerable<Product> GetAll();
    void Update(Product updatedProduct);
}