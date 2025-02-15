# .NET 9 Web API with Swagger & Scalar UI

This repository contains an example .NET 9 Web API demonstrating how to use Swagger to generate an OpenAPI specification and utilize Scalar UI for a user-friendly API documentation interface.

## Overview

This project showcases a basic Web API setup with:

*   **.NET 9:** Built using the latest .NET runtime.
*   **Swagger/OpenAPI:** Uses Swashbuckle.AspNetCore to automatically generate an OpenAPI (Swagger) specification from your API's code.
*   **Scalar UI:** Integrates Scalar UI to provide a visually appealing and interactive documentation experience, rendering the Swagger specification in a clean, modern interface.  Leverages custom extension methods for cleaner configuration.

## Key Components

*   **`Dotnet9.WebApi.Scalar.Swagger.Demo.sln`:** The .NET solution file.
*   **`Dotnet9.WebApi.Scalar.Swagger.Demo.csproj`:** The .NET project file.
*   **`Controllers/`:** Contains the API controllers with decorated actions that Swagger uses to generate the OpenAPI specification.  Pay attention to the use of `[ProducesResponseType]` attributes to provide detailed information about the API responses.
*   **`Contracts/Requests/` & `Contracts/Responses/`:** Defines the request and response DTOs (Data Transfer Objects) used by the API.
*   **`Extensions/OpenApiConfigurationExtensions.cs`:** Contains extension methods for configuring both Swagger and Scalar UI, promoting a cleaner `Program.cs` file.
*   **`Program.cs`:**  Calls the extension methods from `OpenApiConfigurationExtensions.cs` to configure Swagger and Scalar UI.
*   **`Swagger.json`:** The generated OpenAPI specification file.  This file describes your API's endpoints, request/response schemas, and other metadata. It is dynamically generated at runtime.
*   **.config/dotnet-tools.json:** Lists the tools that are local to your project.
*   **`.gitignore`:** Specifies intentionally untracked files that Git should ignore.

## Getting Started

1.  **Clone the Repository:**

    ```bash
    git clone <repository_url>
    cd <repository_name>
    ```

2.  **Install .NET 9 SDK:**

    Ensure you have the .NET 9 SDK installed on your system. You can download it from the official [.NET website](https://dotnet.microsoft.com/en-us/download).

3.  **Restore Dependencies:**

    ```bash
    dotnet restore
    ```

4.  **Build the Project:**

    ```bash
    dotnet build
    ```

5.  **Run the Application:**

    ```bash
    dotnet run
    ```

## Accessing the Documentation

*   **Scalar UI:** After running the application, access the Scalar UI at:

    ```
    http://localhost:5199/swagger/v1/swagger.json
    ```

## Code Highlights

### `Extensions/OpenApiConfigurationExtensions.cs`

This file contains extension methods to encapsulate the Swagger and Scalar UI configuration:

```csharp
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace Dotnet9.WebApi.Scalar.Swagger.Demo.Extensions;

public static class OpenApiConfigurationExtensions
{
    public static void AddOpenApiConfiguration(this IServiceCollection services)
    {
        services.AddOpenApi();

        /* This extension is responsible for crawling the controller action annotations
        to generate the OpenApi specification */
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "v1 Penguins API",
                Version = "v1",
                Description = "An example ASP.NET Core Web API using Swagger with Scalar UI",
            });
        });
    }

    public static void AddScalarUiConfiguration(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        // Enable Swagger
        app.UseSwagger();

        app.MapOpenApi().CacheOutput();

        // Enable Scalar UI
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("v1 Penguins API")
                .WithTheme(ScalarTheme.DeepSpace)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                .WithOpenApiRoutePattern("/swagger/v1/swagger.json"); // Setup Scalar UI to route to swagger.json

            options.Servers ??= new List<ScalarServer>();
            options.Servers.Add(new ScalarServer("http://localhost:5063"));
        });
    }
}
