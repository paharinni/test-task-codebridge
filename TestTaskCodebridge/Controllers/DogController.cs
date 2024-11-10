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
    public IActionResult GetAll()
    {
        var dogs = _context.Dogs
            .Select(d => d.ToDogDto())
            .ToList();

        return Ok(dogs);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var dog = _context.Dogs.Find(id);

        if (dog == null)
        {
            return NotFound();
        }

        return Ok(dog.ToDogDto());
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateDogRequestDto dogDto)
    {
        var dogModel = dogDto.ToDogFromCreateDto();
        _context.Dogs.Add(dogModel);
        _context.SaveChanges();
    
        return CreatedAtAction(nameof(GetById), new { id = dogModel.Id }, dogModel.ToDogDto());
    }
}