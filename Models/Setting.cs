using System.ComponentModel.DataAnnotations;
using pustokApp.Models.Common;

namespace pustokApp.Models;

public class Setting
{
    [Key]
    public string Key { get; set; }
    public string Value { get; set; }
}