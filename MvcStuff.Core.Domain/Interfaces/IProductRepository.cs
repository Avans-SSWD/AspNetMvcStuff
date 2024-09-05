using MvcStuff.Core.Domain.Models;

namespace MvcStuff.Core.Domain.Interfaces;
public interface IProductRepository
{
    void Add(Product product);
    void Delete(int id);
    Product? Get(int id);
    IEnumerable<Product> GetAll();
    void Update(Product updatedProduct);
}