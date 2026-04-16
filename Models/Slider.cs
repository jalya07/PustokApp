using System.ComponentModel.DataAnnotations.Schema;
using pustokApp.Attributes;

namespace pustokApp.Models;

public class Slider
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public string ButtonText { get; set; }
    public string ButtonUrl { get; set; }
    [NotMapped]
    [FileLength(2)]
    [FileTypes("image/jpg", "image/png")]
    public IFormFile File { get; set; }
}