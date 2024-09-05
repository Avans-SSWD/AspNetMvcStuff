using MvcStuff.Core.Domain.Interfaces;
using MvcStuff.Core.Domain.Models;

namespace MvcStuff.Repositories;
public class ProductRepository : IProductRepository
{
    readonly List<Product> _products = new();
    public ProductRepository()
    {
        _products = seedDummyProducts();
    }

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product? Get(int id) => _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product) => _products.Add(product);
    public void Update(Product updatedProduct)
    {
        var product = _products.SingleOrDefault(p => p.Id == updatedProduct.Id);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
        }
    }
    public void Delete(int id) => _products.RemoveAll(p => p.Id == id);

    private List<Product> seedDummyProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.0m, },
            new Product { Id = 2, Name = "Product 2", Price = 12.5m, },
            new Product { Id = 3, Name = "Product 3", Price = 1.3m, },
            new Product { Id = 4, Name = "Product 4", Price = 30.99m, },
            new Product { Id = 5, Name = "Product 5", Price = 21.0m, },
        };
    }



}
