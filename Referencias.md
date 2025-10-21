# Referencias

Tenemos las siguientes carpetas/archivos que corresponden a las capas de nuestra arquitectura:

- **API**: Aquí están los controladores de la API, que exponen los puntos de entrada (endpoints) a través de HTTP.
- **Application**: Servicios de aplicación, lógica de negocio y casos de uso.
- **Domain**: El núcleo de la aplicación, contiene las entidades, repositorios, y las interfaces para los repositorios.
- **Infrastructure**: Implementación concreta de los repositorios, acceso a base de datos, almacenamiento, etc.

## Estructura básica

Esquema básico de la arquitectura que sigue el principio de **separación de responsabilidades**:

```
[API] ---> [Application] --> [Domain] --> [Infrastructure]
```

## Detalles de las relaciones
1. **API**:
   - **Responsabilidad**: Recibe las peticiones HTTP y las dirige a los controladores correspondientes.
   - **Relaciones**:
      - Los controladores de la API dependen de los **Servicios** de la capa de **Aplicación**.
      - Los controladores usan **DTOs** o **modelos de entrada** para recibir los datos.
2. **Application**:
   - **Responsabilidad**: Contiene la lógica de la aplicación y orquesta el flujo de trabajo. Aquí residen los **casos de uso**.
   - **Relaciones**:
      - Los servicios de la capa de **Aplicación** utilizan los **repositorios** definidos en la capa de **Dominio**.
      - Aquí también se pueden defínir validaciones, transformaciones de datos, etc.
      - Los servicios de aplicación se comunican con la capa **API** (controladores) y con la capa **Dominio**.
3. **Domain**:
   - **Responsabilidad**: Contiene las **Entidades** (por ejemplo, `Product` o `Customer`), **Interfaces de Repositorio** y **Lógica de Negocio**.
   - **Relaciones**:
      - **Interfaces de repositorio**: Los repositorios definen las operaciones CRUD (por ejemplo, `IProductRepository`) que se usan para interactuar con la base de datos o cualquier otra fuente de datos. Las implementaciones de estos repositorios están en la capa de **Infrastructure**.
      - **Entidades**:Son las representaciones de objetos de negocio (por ejemplo, `Product`) y su lógica de validación o reglas de negocio.
4. **Infrastructure**:
   - **Responsabilidad**: Implemneta las interfaces de la capa de **Dominio** y maneja detalles como la base de datos, almacenamiento, caché, etc.
   - **Relaciones**:
      - Implementa las interfaces del repositorio definidas en **Domain** (por ejemplo, `ProductRepository` implementa `IProductRepository`).
      - La capa de **Infrastructure** puede usar tecnologías como **Entity Framework** para interactuar con la base de datos.

## Diagrama Visual de Relaciones

### Diagrama de Capas

```
     +---------------+
     |      API      |  <-- (Controladores exponen la API)
     +---------------+
             |
             v
     +---------------+
     |  Application  |  <-- (Servicios de negocio, orquestación)
     +---------------+
             |
             v
     +----------------+
     |     Domain     |  <-- (Entidades, Repositorios, Lógica de negocio)
     +----------------+
             |
             v
     +------------------+
     |  Infrastructure  |  <-- (Implementación concreta, acceso a datos)
     +------------------+
```

### Diagrama de Referencias entre Archivos y Carpetas

```
+------------------+       +------------------+      +--------------------+     +------------------+
|   API Layer      |       | Application Layer|      |   Domain Layer     |     | Infrastructure   |
| (Controllers)    | <---- | (Application     | <--> | (Entities,         | <--> | (Repositories,   |
|                  |       | Services)        |      | Repositories)      |     | Data Access)     |
+------------------+       +------------------+      +--------------------+     +------------------+
        ^                          ^                        ^                         ^
        |                          |                        |                         |
        |                          |                        |                         |
    +-----------+          +-------------------+      +-------------------+      +---------------------+
    | Controllers|          |  Services         |      |  Repositories     |      |  Repositories       |
    | (Api)      |          |  (Application)    |      |  (Domain)         |      |  (Infrastructure)   |
    +-----------+          +-------------------+      +-------------------+      +---------------------+
        |
    +-----------+ 
    |   DTOs    |  <-- (Data Transfer Objects) 
    +-----------+ 
```

### Flujo de Datos

1. **Un cliente hace una solicitud HTTP a un controlador en la API**.
2. El controlador envía la solicitud al **Servicio de Aplicación**, que puede validar los datos, realizar la lógica de negoco y gestionar transacciones.
3. El **Servicio de Aplicación** utiliza los **repositorios** definidos en la capa **Dominio** para obtener o almacenar datos (por ejemplo, un producto).
4. Los **respositorios** están implementados en la capa **Infrastructure**, donde interactúan con la base de datos (por ejemplo, utilizando Entity Framework).
5. Una vez que los datos son recuperados o modificados en **Infrastructure**, se envían de vuelta al servicio de aplicación, y luego al controlador de la API para enviar la respuesta al cliente.

## ¿Cómo se conectan las referencias entre si?

1. **API -> Application**:
    - Los controladores de la API **inyectan** servicios de la capa de **aplicación**.
    - Estos servicios contienen la lógica de negocio y casos de uso, y los controladores los llaman cuando reciben una solicitud.
2. **Application -> Domain**:
    - Los servicios de la **Aplicación** interactuan con las **interfaces de repositorio** definidas en la capa de **Dominio**.
    - Los **repositorios** definen las operaciones CRUD (por ejemplo, `IProductRepository`), pero no contienen la lógica de acceso a datos. Solo definen qué acciones se pueden realizar.
3. **Domain -> Infrastructure**:
    - Los **repositorios** de la cada de **Dominio** son **implementados en la capa de **Infrastructure**.
    - Aquí es donde se maneja la interacción real con la base de datos, ya sea mediante **Entity Framework**, **Dapper**, o cualquier otra tecnologia de acceso a datos.

## Código de Ejemplo de las Relaciones

1. **API Layer (Controlador)**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
}
```

2. **Application Layer (Servicio)**
```csharp
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => new ProductDto(p));
    }
}
```

3. **Domain Layer (Repositorio y Entidad)**

```csharp
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
}

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

4. **Infrastructure Layer (Implementación del repositorio)**

```csharp
public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new List<Product>();

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await Task.FromResult(_products);
    }
}
```