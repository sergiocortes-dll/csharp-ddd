# DDD con C# - Guía

## Crear la solución y los proyectos

Crea una carpeta, desde la que vamos a trabajar.

Para trabjar con DDD, necesitaremos Domain, Application, Infrastructure y API.

### Crear los proyectos
Nuestro proyecto se llamará `webApi`.
```bash
dotnet new classlib -n webApi.Domain
dotnet new classlib -n webApi.Application
dotnet new classlib -n webApi.Infrastructure

dotnet new webapi -n webApi.Api
```

Ahora mismo nuestro proyecto deberia lucir algo asi:

```
webApi
|-- webApi.Api
|-- webApi.Application
|-- webApi.Domain
|-- webApi.Infrastructure
```

### Agregar los proyectos a la solución

En la carpeta raíz (`webApi`) si **NO** existe, crear el archivo `.sln` de la solución.

```
dotnet new sln
```

**Agregar los proyectos a la solución**

```bash
dotnet sln . add webApi.Api
dotnet sln . add webApi.Application
dotnet sln . add webApi.Domain
dotnet sln . add webApi.Infrastructure
```

### Agregar referencias entre proyectos

Esto es para poder usar los recursos de un proyecto en otro.
Mas información sobre referencias en: [Referencias.md](Referencias.md)

```bash
dotnet add apiWeb.Application reference apiWeb.Domain
dotnet add apiWeb.Infrastructure reference apiWeb.Domain
dotnet add apiWeb.Api reference apiWeb.Application
dotnet add apiWeb.Api reference apiWeb.Infrastructure  # opcional para DI
```

¿Por qué de `apiWeb.Api` -> `apiWeb.Infrastructure` es opcional?
Porque estamos vamos a trabajar con interfaces que vamos a inyectar en el program de `apiWeb.Api`, si no vas a trabjar con interfaces, no es necesario hacer la ultima relación.

## Crear una entidad en `Domain`

Para hacerlo simple, haremos una entidad `Customer` con `Id`, `Name`, `Email` y `Age`.

_¿Por qué en `Entities` y no en `Models`?_

- **Entities** es la carpeta que normalmente usamos para las clases que representan las **entidades del dominio**, es decir, objetos con identidad propia y reglas de negocio.
- **Models** suele usarse más en proyectos web o API para representar datos de entrada o salida (DTOs), o en aplicaciones más tradicionales, pero no es tan específico para DDD.

Asi se distingue claramente la lógica central del dominio (entidades, agregados, valores) de otras capas, como Application o Infrastructure.

---

**apiWeb.Domain/Entities/Customer.cs**

```csharp
namespace apiWeb.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
```


