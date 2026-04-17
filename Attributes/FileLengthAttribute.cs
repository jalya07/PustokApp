using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace pustokApp.Attributes;

public class FileLengthAttribute: ValidationAttribute
{
    public int Length { get; set; }
     public FileLengthAttribute(int length)
    {
        Length = length;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > Length * 1024 * 1024)
                return new ValidationResult($"File size cannot exceed {Length}MB.");
        }
        return ValidationResult.Success;
    }
}