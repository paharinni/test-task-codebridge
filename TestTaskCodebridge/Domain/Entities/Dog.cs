using System.ComponentModel.DataAnnotations;

namespace TestTaskCodebridge.Domain.Entities;

public class Dog
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters.")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabetical characters.")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(20, ErrorMessage = "Color must be no longer than 20 characters.")]
    public string Color { get; set; } = string.Empty;
    [Range(0, 100, ErrorMessage = "Tail length must be be between 0 and 100cm")]
    public int TailLength { get; set; }
    [Range(0, 100000, ErrorMessage = "Weight must be between 0 and 100,000g")]
    public int Weight { get; set; }
}