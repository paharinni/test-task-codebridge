using Microsoft.AspNetCore.Mvc;
using TestTaskCodebridge.Database;
using TestTaskCodebridge.Domain.Dtos;
using TestTaskCodebridge.Domain.Mappers;
using TestTaskCodebridge.Interfaces;
using TestTaskCodebridge.Services.Helpers;

namespace TestTaskCodebridge.Controllers;

[ApiController]
[Route("api/dog")]
public class DogController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IDogRepository _dogRepository;

    public DogController(AppDbContext context, IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var dogs = await _dogRepository.GetAllAsync(query);
        var dogDto = dogs.Select(d => d.ToDogDto());
        return Ok(dogs);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var dog = await _dogRepository.GetByIdAsync(id);

        if (dog == null)
        {
            return NotFound();
        }

        return Ok(dog.ToDogDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDogRequestDto dogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var dogModel = dogDto.ToDogFromCreateDto();
        await _dogRepository.CreateAsync(dogModel);
        return CreatedAtAction(nameof(GetById), new { id = dogModel.Id }, dogModel.ToDogDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDogRequestDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var dogModel = await _dogRepository.UpdateAsync(id, updateDto);
        
        if (dogModel == null)
        {
            return NotFound();
        }

        return Ok(dogModel.ToDogDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var dogModel = await _dogRepository.DeleteAsync(id);
            
        if (dogModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}