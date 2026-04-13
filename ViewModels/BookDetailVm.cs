namespace pustokApp.ViewModels;

public class BookDetailVm
{
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int DiscountedPrice { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool InStock { get; set; }
        public bool InFeatured { get; set; }
        public bool IsNew { get; set; }
        public List<string> Images { get; set; }
}