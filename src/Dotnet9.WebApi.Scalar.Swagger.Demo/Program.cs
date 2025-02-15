using Dotnet9.WebApi.Scalar.Swagger.Demo.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApiConfiguration();

var app = builder.Build();

app.AddScalarUiConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();