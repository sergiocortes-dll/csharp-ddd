using apiWeb.Application.Services;
using apiWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apiWeb.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    
    // Inject from ProductService
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    
    // Get all ENDPOINT
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAllProductAsync();
        if (products == null || !products.Any())
            return NoContent();
        
        return Ok(products);
    }
    
    // Create a Product ENDPOINT
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        if (product == null)
            return BadRequest("Invalid product.");

        await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }
    
    // Get by ID ENDPOINT
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}