using Microsoft.EntityFrameworkCore;
using TestTaskCodebridge.Database;
using TestTaskCodebridge.Domain.Dtos;
using TestTaskCodebridge.Domain.Entities;
using TestTaskCodebridge.Interfaces;

namespace TestTaskCodebridge.Repository;

public class DogRepository : IDogRepository
{
    private readonly AppDbContext _context;

    public DogRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Dog>> GetAllAsync()
    {
        return await _context.Dogs.ToListAsync();
    }

    public async Task<Dog?> GetByIdAsync(int id)
    {
        return await _context.Dogs.FindAsync(id);
    }

    public async Task<Dog> CreateAsync(Dog dogModel)
    {
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