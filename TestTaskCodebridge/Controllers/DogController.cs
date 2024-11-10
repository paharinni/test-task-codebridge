using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskCodebridge.Database;
using TestTaskCodebridge.Domain.Dtos;
using TestTaskCodebridge.Domain.Entities;
using TestTaskCodebridge.Domain.Mappers;

namespace TestTaskCodebridge.Controllers;

[ApiController]
[Route("api/dog")]
public class DogController : ControllerBase
{
    private readonly AppDbContext _context;

    public DogController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var dogs = await _context.Dogs
            .Select(d => d.ToDogDto())
            .ToListAsync();

        return Ok(dogs);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var dog = await _context.Dogs.FindAsync(id);

        if (dog == null)
        {
            return NotFound();
        }

        return Ok(dog.ToDogDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDogRequestDto dogDto)
    {
        var dogModel = dogDto.ToDogFromCreateDto();
        await _context.Dogs.AddAsync(dogModel);
        await _context.SaveChangesAsync();
    
        return CreatedAtAction(nameof(GetById), new { id = dogModel.Id }, dogModel.ToDogDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDogRequestDto updateDto)
    {
        var dogModel = await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);
        
        if (dogModel == null)
        {
            return NotFound();
        }

        dogModel.Name = updateDto.Name;
        dogModel.Color = updateDto.Color;
        dogModel.Weight = updateDto.Weight;
        dogModel.TailLength = updateDto.TailLength;

        await _context.SaveChangesAsync();

        return Ok(dogModel.ToDogDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var dogModel = await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);

        if (dogModel == null)
        {
            return NotFound();
        }

        _context.Dogs.Remove(dogModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}