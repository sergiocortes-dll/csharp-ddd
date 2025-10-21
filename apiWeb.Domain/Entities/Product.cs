namespace apiWeb.Domain.Entities;

public class Product
{
    // Guid = Unique global identifier
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public Product(Guid id, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or whitespace.", nameof(name));
        if (price < 0)
            throw new ArgumentException("Product price cannot be negative.", nameof(price));

        Id = id;
        Name = name;
        Price = price;
    }
}