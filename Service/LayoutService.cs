using pustokApp.Data;

namespace pustokApp.Service;

public class LayoutService(PustokAppDbContext context)
{
    public  Dictionary<string, string> GetSettings()
    {
        return context.Settings.ToDictionary(x => x.Key, x => x.Value);
    }
    
}