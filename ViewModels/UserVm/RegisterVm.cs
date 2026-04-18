using System.ComponentModel.DataAnnotations;

namespace pustokApp.ViewModels.UserVm;

public class RegisterVm
{
    [Microsoft.Build.Framework.Required]
    public string FullName { get; set; }
    [Microsoft.Build.Framework.Required]
    public string UserName { get; set; }
    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}