using System.ComponentModel.DataAnnotations;

namespace pustokApp.ViewModels.UserVm;

public class LoginVm
{
    [Microsoft.Build.Framework.Required]
    public string UserNameOrEmail{ get; set; }
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}