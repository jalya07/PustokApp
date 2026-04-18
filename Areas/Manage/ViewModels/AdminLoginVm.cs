using System.ComponentModel.DataAnnotations;

namespace pustokApp.Areas.Manage.ViewModels;

public class AdminLoginVm
{
    [Microsoft.Build.Framework.Required]
    public string Username { get; set; }
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}