using apiWeb.Application.Services;
using apiWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apiWeb.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;
    
    // Inject from CustomerService
    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }
    
    // Get all ENDPOINT
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _customerService.GetAllCustomerAsync();
        if (customers == null || !customers.Any())
            return NoContent();

        return Ok(customers);
    }
    
    // Create a Product ENDPOINT
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
        if (customer == null)
            return BadRequest("Invalid customer.");

        await _customerService.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
    }
    
    // Get by ID ENDPOINT
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }
}