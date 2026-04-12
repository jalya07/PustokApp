namespace pustokApp.ViewModels;

public class BookVm
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
}