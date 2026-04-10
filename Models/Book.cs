namespace pustokApp.Models;

public class Book
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
        public int Code { get; set; }
        public int DiscountPercent { get; set; }
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public string MainUrl { get; set; }
        public string HoverUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BookImage> BookImages { get; set; }
        public List<BookTag> BookTags { get; set; }
}