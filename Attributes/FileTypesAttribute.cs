using System.ComponentModel.DataAnnotations;

namespace pustokApp.Attributes;

public class FileTypesAttribute: ValidationAttribute
{
    public string[] FileTypes { get; set; }
    public  FileTypesAttribute(params string[] fileTypes)
    {
        FileTypes = fileTypes;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).TrimStart('.').ToLower();
            if (!FileTypes.Any(ft => ft.ToLower() == ext))
                return new ValidationResult($"Allowed types: {string.Join(", ", FileTypes)}");
        }
        return ValidationResult.Success;
    }
}