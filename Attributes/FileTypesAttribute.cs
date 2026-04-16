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
        if (files != null)
        {            
            foreach (var file in files)
            {                
                if (!FileTypes.Contains(file.ContentType))
                {                    
                    return new ValidationResult($"File type must be one of the following: {string.Join(", ", FileTypes)}");
                }
            }
        }
        return base.IsValid(value, validationContext);
    }
}