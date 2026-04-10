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
        base.OnModelCreating(modelBuilder);
    }

    public PustokAppDbContext(DbContextOptions<PustokAppDbContext> options) : base(options)
    {
        
    }
}