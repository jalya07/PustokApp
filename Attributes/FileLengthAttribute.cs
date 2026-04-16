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
        return base.IsValid(value, validationContext);
    }
}