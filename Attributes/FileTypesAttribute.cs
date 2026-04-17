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
        List<IFormFile> files = value as List<IFormFile>;
        if (files != null && files.Count > 0)
        {            
            foreach (var file in files)
            {
                // Get file extension without the dot
                var fileExtension = Path.GetExtension(file.FileName).TrimStart('.').ToLower();
                
                // Check if extension is in the allowed list
                if (!FileTypes.Any(ft => ft.ToLower() == fileExtension))
                {                    
                    return new ValidationResult($"File type must be one of the following: {string.Join(", ", FileTypes)}");
                }
            }
        }
        return ValidationResult.Success;
    }
}