using System.Collections.Concurrent;
using Dotnet9.WebApi.Scalar.Swagger.Demo.Contracts.Requests;
using Dotnet9.WebApi.Scalar.Swagger.Demo.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.WebApi.Scalar.Swagger.Demo.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]")]
public class PenguinsController : ControllerBase
{
    private static readonly ConcurrentBag<PenguinResponseDto> Penguins = [];

    public PenguinsController()
    {
        if (Penguins.IsEmpty)
        {
            Penguins.Add(new PenguinResponseDto(Guid.CreateVersion7(), "Captain", "Emperor", 10));
            Penguins.Add(new PenguinResponseDto(Guid.CreateVersion7(), "Kowalski", "Adelie", 7));
        }
    }
    
    [HttpGet("{id:guid:required}", Name = nameof(GetPenguinById))]
    [ProducesResponseType(typeof(PenguinResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetPenguinById(Guid id)
    {
        var penguin = Penguins.FirstOrDefault(p => p.Id == id);

        if (penguin is null) return NotFound();

        return Ok(penguin);
    }
    
    [HttpGet(Name = nameof(GetPenguins))]
    [ProducesResponseType(typeof(IReadOnlyList<PenguinResponseDto>), StatusCodes.Status200OK)]
    public IActionResult GetPenguins()
    {
        return Ok(Penguins.ToList());
    }
    
    [HttpPost(Name = nameof(CreatePenguin))]
    [ProducesResponseType(typeof(PenguinResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreatePenguin([FromBody] CreatePenguinRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newPenguin = new PenguinResponseDto(
            Guid.NewGuid(),
            request.Name,
            request.Species,
            request.Age);
        
        Penguins.Add(newPenguin);

        return CreatedAtAction(nameof(CreatePenguin), new { id = newPenguin.Id }, newPenguin);
    }
}