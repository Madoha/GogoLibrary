using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GogoLibrary.DAL.Configurations;

public class BookCommentConfiguration : IEntityTypeConfiguration<BookComment>
{
    public void Configure(EntityTypeBuilder<BookComment> builder)
    {
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(b => b.Content).IsRequired().HasMaxLength(500);
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.BookId).IsRequired();
    }
}