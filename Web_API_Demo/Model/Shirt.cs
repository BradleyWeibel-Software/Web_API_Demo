using System.ComponentModel.DataAnnotations;
using Web_API_Demo.Validation;

namespace Web_API_Demo.Model
{
    public class Shirt
    {
        public int? Id { get; set; }
        [Required]
        public string? Brand { get; set; }
        public string? Colour { get; set; }
        [Shirt_EnsureCorrectSizing]
        public int Size { get; set; }
        [Required]
        public string? Sex { get; set; }
    }
}
