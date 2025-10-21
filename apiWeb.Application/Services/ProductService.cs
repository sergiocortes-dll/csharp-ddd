using apiWeb.Domain.Entities;
using apiWeb.Domain.Repositories;

namespace apiWeb.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    
    // Injection
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Product?> GetProductByIdAsync(Guid id)
    {
        return _productRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Product>> GetAllProductAsync()
    {
        return _productRepository.GetAllAsync();
    }

    public Task AddProductAsync(Product product)
    {
        return _productRepository.AddAsync(product);
    }

    public Task UpdateProductAsync(Product product)
    {
        return _productRepository.UpdateAsync(product);
    }

    public Task DeleteProductAsync(Guid id)
    {
        return _productRepository.DeleteAsync(id);
    }
}