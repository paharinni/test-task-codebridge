using Microsoft.EntityFrameworkCore;
using TestTaskCodebridge.Database;
using TestTaskCodebridge.Domain.Dtos;
using TestTaskCodebridge.Domain.Entities;
using TestTaskCodebridge.Interfaces;
using TestTaskCodebridge.Services.Helpers;

namespace TestTaskCodebridge.Repository;

public class DogRepository : IDogRepository
{
    private readonly AppDbContext _context;

    public DogRepository(AppDbContext context)
    {
        _context = context;
    }
    
    // properties are also not sure
    public async Task<List<Dog>> GetAllAsync(QueryObject query)
    {
        var dogs = _context.Dogs.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            dogs = dogs.Where(d => d.Name.Contains(query.Name));
        }
        if (!string.IsNullOrWhiteSpace(query.Color))
        {
            dogs = dogs.Where(d => d.Color.Contains(query.Color));
        }
        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Color", StringComparison.OrdinalIgnoreCase))
            {
                dogs = query.IsDescending ? dogs.OrderByDescending(d => d.Color) : dogs.OrderBy(d => d.Color);
            }
        }

        return await dogs.ToListAsync();
    }

    public async Task<Dog?> GetByIdAsync(int id)
    {
        return await _context.Dogs.FindAsync(id);
    }

    public async Task<Dog> CreateAsync(Dog dogModel)
    {
        if (await _context.Dogs.AnyAsync(d => d.Name == dogModel.Name))
        {
            throw new InvalidOperationException("A dog with this name already exists.");
        }
        await _context.Dogs.AddAsync(dogModel);
        await _context.SaveChangesAsync();

        return dogModel;
    }

    public async Task<Dog?> UpdateAsync(int id, UpdateDogRequestDto dogDto)
    {
        var existingDog = await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);
        
        if (existingDog == null)
        {
            return null;
        }
        if (await _context.Dogs.AnyAsync(d => d.Name == dogDto.Name))
        {
            throw new InvalidOperationException("A dog with this name already exists.");
        }
        
        
        existingDog.Name = dogDto.Name;
        existingDog.Color = dogDto.Color;
        existingDog.Weight = dogDto.Weight;
        existingDog.TailLength = dogDto.TailLength;

        await _context.SaveChangesAsync();

        return existingDog;
    }

    public async Task<Dog?> DeleteAsync(int id)
    {
        var dogModel =  await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);
        
        if (dogModel == null)
        {
            return null;
        }

        _context.Dogs.Remove(dogModel);

        await _context.SaveChangesAsync();

        return dogModel;
    }
}