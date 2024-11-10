using System.ComponentModel.DataAnnotations;

namespace TestTaskCodebridge.Domain.Entities;

public class Dog
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Color { get; set; } = string.Empty;
    [Range(0, int.MaxValue, ErrorMessage = "Tail length must be a non-negative number.")]
    public int TailLength { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Weight must be a non-negative number.")]
    public int Weight { get; set; }
}