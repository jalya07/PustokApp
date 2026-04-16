using System.ComponentModel.DataAnnotations;

namespace pustokApp.Models;

public class Author
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Full Name must be between 2 and 20 characters")]
    public string FullName { get; set; }
    
    public List<Book> Books { get; set; }
}