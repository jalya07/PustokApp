using pustokApp.Models;

namespace pustokApp.ViewModels;

public class BookVm
{
    public Book Book { get; set; }
    public List<Book> RelatedBooks { get; set; }
}