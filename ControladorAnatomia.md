```csharp
 1 using apiWeb.Application.Services;
 2 using apiWeb.Domain.Entities;
 3 using Microsoft.AspNetCore.Mvc;
 4
 5 namespace apiWeb.Api.Controllers;
 6
 7 [Route("api/[controller]")]
 8 [ApiController]
 9 public class CustomerController : ControllerBase
10 {
11    private readonly CustomerService _customerService;
12    
13    // Inject from CustomerService
14    public CustomerController(CustomerService customerService)
15    {
16        _customerService = customerService;
17    }
18    
19    // Get all ENDPOINT
20    [HttpGet]
21    public async Task<IActionResult> Get()
22    {
23        var customers = await _customerService.GetAllCustomerAsync();
24        if (customers == null || !customers.Any())
25            return NoContent();
26
27        return Ok(customers);
28    }
29    
30    // Create a Product ENDPOINT
31    [HttpPost]
32    public async Task<IActionResult> Post([FromBody] Customer customer)
33    {
34        if (customer == null)
35            return BadRequest("Invalid product.");
36
37        await _customerService.AddCustomerAsync(customer);
38        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
39    }
40    
41    // Get by ID ENDPOINT
42    [HttpGet("{id}")]
43    public async Task<IActionResult> GetById(int id)
44    {
45        var customer = await _customerService.GetProductByIdAsync(id);
46        if (customer == null)
47            return NotFound();
48
49        return Ok(customer);
50    }
51 }
```

## 7
Para especificar la ruta base de la API, en este caso sería `localhost:XXXX/api/Customer`.

## 9
Heredamos `ControllerBase`, a diferencia de en MVC que heredabamos `Controller`.

## 11-12
```csharp
private readonly CustomerService _customerService;
```

Se declara una variable privada y de solo lectura (readonly) del tipo CustomerService. Esto es parte de la inyección de dependencias.

## 14-17
```csharp
public CustomerController(CustomerService customerService)
{
    _customerService = customerService;
}
```

Constructor del controlador, donde se inyecta una instancia de CustomerService. ASP.NET Core se encarga de pasar esta dependencia al controlador automáticamente si está registrada en el Startup o Program.

## 20-28 – Método GET (Obtener todos los clientes)
```csharp
[HttpGet]
public async Task<IActionResult> Get()
{
var customers = await _customerService.GetAllCustomerAsync();
if (customers == null || !customers.Any())
return NoContent();

    return Ok(customers);
}
```

Endpoint HTTP GET para obtener todos los clientes.

Si no hay clientes, devuelve 204 NoContent.

Si existen, devuelve 200 OK con la lista.

## 31-39 – Método POST (Crear un nuevo cliente)
````csharp
[HttpPost]
public async Task<IActionResult> Post([FromBody] Customer customer)
{
if (customer == null)
return BadRequest("Invalid customer.");

    await _customerService.AddCustomerAsync(customer);
    return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
}
````

Endpoint HTTP POST para crear un nuevo cliente.

Recibe un objeto Customer desde el cuerpo del request ([FromBody]).

Si es nulo, retorna 400 BadRequest.

Si es válido, lo guarda usando el servicio y responde con 201 Created, indicando dónde se puede obtener el recurso.

## 42-50 – Método GET por ID
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var customer = await _customerService.GetCustomerByIdAsync(id);
    if (customer == null)
        return NotFound();

    return Ok(customer);
}
```

Endpoint HTTP GET con parámetro id.

Busca un cliente por su identificador.

Si no existe, devuelve 404 Not Found.

Si lo encuentra, devuelve 200 OK con el cliente.
