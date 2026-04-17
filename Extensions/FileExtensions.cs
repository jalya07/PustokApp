using Microsoft.AspNetCore.Http;

namespace pustokApp.Extensions;

public static class FileExtensions
{
    public static string SaveFile(this IFormFile file, string webRootPath)
    {
        if (file == null || file.Length == 0)
            return null;

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var imagePath = Path.Combine("assets", "image", "books");
        var path = Path.Combine(webRootPath, imagePath, fileName);

        // Create directory if it doesn't exist
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return fileName;
    }
}
