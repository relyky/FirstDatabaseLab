
# Project Overview

This is a .NET 8 Web API project created using the default template. It includes a single `WeatherForecastController` that returns a random weather forecast. The project is configured to use Swashbuckle for OpenAPI and Swagger UI, which provides interactive API documentation.

## Building and Running

To build and run this project, you can use the following .NET CLI commands:

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the project
dotnet run
```

The API will be available at `https://localhost:7195/swagger` (the port may vary).

## Development Conventions

*   The project follows the standard .NET 8 Web API conventions.
*   Controllers are located in the `Controllers` directory.
*   Data Transfer Objects (DTOs) are located in the `DTO` directory.
*   The project uses implicit usings and nullable reference types.
