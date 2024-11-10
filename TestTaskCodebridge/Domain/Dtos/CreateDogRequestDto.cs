namespace TestTaskCodebridge.Domain.Dtos;

public class CreateDogRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int TailLength { get; set; }
    public int Weight { get; set; }
}