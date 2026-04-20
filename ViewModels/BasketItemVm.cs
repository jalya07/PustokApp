namespace pustokApp.ViewModels;

public class BasketItemVm
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookName { get; set; }
    public decimal BookPrice { get; set; }
    public int Count { get; set; }
    public string MainImageUrl { get; set; }
}