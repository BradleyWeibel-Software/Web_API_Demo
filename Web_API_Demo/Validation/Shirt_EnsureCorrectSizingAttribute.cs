using System.ComponentModel.DataAnnotations;
using Web_API_Demo.Model;

namespace Web_API_Demo.Validation
{
    public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var shirt = validationContext.ObjectInstance as Shirt;

            if (shirt != null && !string.IsNullOrWhiteSpace(shirt.Sex))
            {
                if (shirt.Sex.Equals("male", StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
                    return new ValidationResult("For male shirts, the size must be >= 8");
                else if (shirt.Sex.Equals("female", StringComparison.OrdinalIgnoreCase) && shirt.Size < 6)
                    return new ValidationResult("For female shirts, the size must be >= 6");
            }

            return ValidationResult.Success;
        }
    }
}
