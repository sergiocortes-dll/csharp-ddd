using apiWeb.Domain.Entities;
using apiWeb.Domain.Repositories;

namespace apiWeb.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new List<Customer>();
    public Task<Customer?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}