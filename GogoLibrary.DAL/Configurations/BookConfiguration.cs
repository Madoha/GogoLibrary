using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GogoLibrary.DAL.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Author).IsRequired();
        
        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Book)
            .HasForeignKey(x => x.BookId);
    }
}