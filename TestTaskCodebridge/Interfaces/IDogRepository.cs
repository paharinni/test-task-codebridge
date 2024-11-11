using TestTaskCodebridge.Domain.Dtos;
using TestTaskCodebridge.Domain.Entities;
using TestTaskCodebridge.Services.Helpers;

namespace TestTaskCodebridge.Interfaces;

public interface IDogRepository
{
    Task<List<Dog>> GetAllAsync(QueryObject query);
    Task<Dog?> GetByIdAsync(int id);
    Task<Dog> CreateAsync(Dog dogModel);
    Task<Dog?> UpdateAsync(int id, UpdateDogRequestDto dogDto);
    Task<Dog?> DeleteAsync(int id);
}