using apiWeb.Domain.Entities;
using apiWeb.Domain.Repositories;

namespace apiWeb.Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;
    
    // Injection
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<IEnumerable<Customer>> GetAllCustomerAsync()
    {
        return _customerRepository.GetAllAsync();
    }
    
    public Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return _customerRepository.GetByIdAsync(id);
    }

    public Task AddCustomerAsync(Customer customer)
    {
        return _customerRepository.AddAsync(customer);
    }

    public Task UpdateCustomerAsync(Customer customer)
    {
        return _customerRepository.UpdateAsync(customer);
    }

    public Task DeleteCustomerAsync(int id)
    {
        return _customerRepository.DeleteAsync(id);
    }
}