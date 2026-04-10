using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pustokApp.Models;

namespace pustokApp.Data.Configuration;

public class BookTagConfiguration:IEntityTypeConfiguration<BookTag>
{
    public void Configure(EntityTypeBuilder<BookTag> builder)
    {
        builder.HasKey(x => new {x.BookId, x.TagId});
    }
}