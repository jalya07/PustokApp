using Microsoft.EntityFrameworkCore;
using pustokApp.Models;

namespace pustokApp.Data;

public class PustokAppDbContext:DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookImage> BookImages { get; set; }
    public DbSet<BookTag> BookTags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Slider> Sliders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        // Seed Authors
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FullName = "H.G. Wells" },
            new Author { Id = 2, FullName = "J.D. Kurtness" }
        );

        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Name = "De Vengeance",
                Desc = "Cover Up Front Of Books And Leave Summary",
                Price = 78.09,
                Code = 1001,
                DiscountPercent = 10,
                InStock = true,
                IsFeatured = true,
                IsNew = true,
                MainUrl = "~/image/products/product-1-1.jpg",
                HoverUrl = "~/image/products/product-1-2.jpg",
                AuthorId = 1
            },
            new Book
            {
                Id = 2,
                Name = "De Vengeance",
                Desc = "Cover Up Front Of Books And Leave Summary",
                Price = 78.09,
                Code = 1002,
                DiscountPercent = 5,
                InStock = true,
                IsFeatured = true,
                IsNew = false,
                MainUrl = "~/image/products/product-2-1.jpg",
                HoverUrl = "~/image/products/product-2-2.jpg",
                AuthorId = 2
            }
        );

        base.OnModelCreating(modelBuilder);
    }

    public PustokAppDbContext(DbContextOptions<PustokAppDbContext> options) : base(options)
    {
        
    }
}