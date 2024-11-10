using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.Models
{
    public class Dog
    {
        public int Id { get; set; }

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