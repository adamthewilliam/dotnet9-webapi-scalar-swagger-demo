namespace Dotnet9.WebApi.Scalar.Swagger.Demo.Contracts.Requests;

public record CreatePenguinRequestDto(Guid Id, string Name, string Species, int Age);