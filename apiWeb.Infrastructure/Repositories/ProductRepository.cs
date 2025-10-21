using apiWeb.Domain.Entities;
using apiWeb.Domain.Repositories;

namespace apiWeb.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new List<Product>();

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(_products.Find(p => p.Id == id));
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        foreach (var product in _products)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
        }
        return await Task.FromResult((IEnumerable<Product>)_products);
    }


    public async Task AddAsync(Product product)
    {
        _products.Add(product);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Product product)
    {
        var existingProduct = _products.Find(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            // TODO
        }


        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var productToRemove = _products.Find(p => p.Id == id);
        if (productToRemove != null)
        {
            _products.Remove(productToRemove);
        }

        await Task.CompletedTask;
    }
}