using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogsHouseService.Data;
using DogsHouseService.Models;

namespace DogsHouseService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogsController : ControllerBase
    {
        private readonly DogsHouseContext _context;

        public DogsController(DogsHouseContext context)
        {
            _context = context;
        }

        // GET /dogs
        [HttpGet]
        public async Task<IActionResult> GetDogs(
            [FromQuery] string? attribute,
            [FromQuery] string? order,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            IQueryable<Dog> query = _context.Dogs;

            // Sorting
            if (!string.IsNullOrEmpty(attribute))
            {
                bool descending = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase);

                query = attribute.ToLower() switch
                {
                    "name" => descending ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name),
                    "color" => descending ? query.OrderByDescending(d => d.Color) : query.OrderBy(d => d.Color),
                    "tail_length" => descending ? query.OrderByDescending(d => d.Tail_Length) : query.OrderBy(d => d.Tail_Length),
                    "weight" => descending ? query.OrderByDescending(d => d.Weight) : query.OrderBy(d => d.Weight),
                    _ => query
                };
            }

            // Pagination
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                int skip = (pageNumber.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            var dogs = await query.Select(d => new
            {
                d.Name,
                d.Color,
                d.Tail_Length,
                d.Weight
            }).ToListAsync();

            return Ok(dogs);
        }

        // POST /dogs
        [HttpPost]
        public async Task<IActionResult> CreateDog([FromBody] DogCreateDto dogDto)
        {
            if (dogDto == null)
            {
                return BadRequest("Invalid JSON payload.");
            }

            // Model validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if a dog with the same name exists
            if (await _context.Dogs.AnyAsync(d => d.Name == dogDto.Name))
            {
                return Conflict($"A dog with the name '{dogDto.Name}' already exists.");
            }

            var dog = new Dog
            {
                Name = dogDto.Name,
                Color = dogDto.Color,
                Tail_Length = dogDto.Tail_Length,
                Weight = dogDto.Weight
            };

            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDogs), new { id = dog.Id }, dog);
        }
    }

    // DTO for creating a dog
    public class DogCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tail length must be a non-negative number.")]
        public int Tail_Length { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Weight must be a non-negative number.")]
        public int Weight { get; set; }
    }
}
