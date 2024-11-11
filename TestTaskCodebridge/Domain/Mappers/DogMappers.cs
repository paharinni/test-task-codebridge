using TestTaskCodebridge.Domain.Dtos;
using TestTaskCodebridge.Domain.Entities;

namespace TestTaskCodebridge.Domain.Mappers;

public static class DogMappers
{
    public static DogDto ToDogDto(this Dog dogModel)
    {
        return new DogDto()
        {
            Name = dogModel.Name,
            Color = dogModel.Color,
            Weight = dogModel.Weight,
            Tail_Length = dogModel.Tail_Length
        };
    }

    public static Dog ToDogFromCreateDto(this CreateDogRequestDto dogDto)
    {
        return new Dog()
        {
            Name = dogDto.Name,
            Color = dogDto.Color,
            Weight = dogDto.Weight,
            Tail_Length = dogDto.Tail_Length
        };
    }
}