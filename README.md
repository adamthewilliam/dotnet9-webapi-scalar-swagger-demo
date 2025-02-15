# .NET 9 Web API with Swagger & Scalar UI

This repository contains an example .NET 9 Web API demonstrating how to use Swagger to generate an OpenAPI specification and utilize Scalar UI for a user-friendly API documentation interface.

## Overview

This project showcases a basic Web API setup with:

*   **.NET 9:** Built using the latest .NET runtime.
*   **Swagger/OpenAPI:** Uses Swashbuckle.AspNetCore to automatically generate an OpenAPI (Swagger) specification from your API's code.
*   **Scalar UI:** Integrates Scalar UI to provide a visually appealing and interactive documentation experience, rendering the Swagger specification in a clean, modern interface.

## Key Components

*   **`Alertu.ScalarSwagger.Api.csproj`:** The .NET project file.
*   **`Controllers/`:** Contains the API controllers with decorated actions that Swagger uses to generate the OpenAPI specification.  Pay attention to the use of `[ProducesResponseType]` attributes to provide detailed information about the API responses.
*   **`Contracts/Requests/` & `Contracts/Responses/`:** Defines the request and response DTOs (Data Transfer Objects) used by the API.
*   **`Program.cs`:**  Configures Swagger and Scalar UI within the application's pipeline. This is where you'll find the settings for the `swagger.json` endpoint and the Scalar UI middleware.
*   **`Swagger.json`:** The generated OpenAPI specification file.  This file describes your API's endpoints, request/response schemas, and other metadata.  It is generated on build.
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

*   **Swagger UI:** After running the application, access the default Swagger UI at:

    ```
    http://localhost:<port>/swagger
    ```

*   **Scalar UI:** Access the Scalar UI at:

    ```
    http://localhost:<port>/scalar
    ```

    (Replace `<port>` with the actual port your application is running on, typically 5000 or 5001).

## Generating the `swagger.json` on Build (Important!)

This project is configured to automatically generate the `swagger.json` file during the build process. To achieve this, the following steps are required:

1.  **Install the `Swashbuckle.AspNetCore.Cli` as a Local Tool:**

    ```bash
    dotnet new tool-manifest  # Creates a .config/dotnet-tools.json file
    dotnet tool install --local Swashbuckle.AspNetCore.Cli
    ```

2.  **The .csproj has the following XML added:**

    ```xml
     <Target Name="OpenAPI" AfterTargets="Build" Condition="'$(Configuration)'=='Debug'">
        <Exec Command="dotnet swagger tofile --output Swagger.json bin/Debug/net9.0/Alertu.ScalarSwagger.Api.dll v1" WorkingDirectory="$(ProjectDir)" EnvironmentVariables="DOTNET_ROLL_FORWARD=LatestMajor" />
    </Target>
    ```

## Configuration

### Swagger

Swagger is configured using Swashbuckle.AspNetCore.  You can customize the Swagger generation process by modifying the `AddSwaggerGen` options in `Program.cs`.

### Scalar UI

Scalar UI is configured via the `app.UseScalar()` middleware in `Program.cs`.  The key setting is `options.SpecUrl`, which specifies the location of the `swagger.json` file.  You can also customize the document title and other Scalar UI options here.

## Contributing

Feel free to contribute to this project by submitting pull requests with improvements, bug fixes, or new features.

## License

This project is licensed under the [MIT License](LICENSE).
