namespace pustokApp.Extensions;

public class FileManager
{
    public static string SaveFile(IFormFile file, string rootPath)
    {
        if(!file.ContentType.Contains("image/"))
            return null;
        
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string path = Path.Combine(rootPath, fileName);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return fileName;
    }
}