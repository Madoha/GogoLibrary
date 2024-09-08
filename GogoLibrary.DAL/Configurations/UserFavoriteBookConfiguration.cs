using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GogoLibrary.DAL.Configurations;

public class UserFavoriteBookConfiguration : IEntityTypeConfiguration<UserFavoriteBook>
{
    public void Configure(EntityTypeBuilder<UserFavoriteBook> builder)
    {
        builder.HasKey(ufb => new { ufb.UserId, ufb.BookId });
        
        builder.HasOne(ufb => ufb.User)
            .WithMany(u => u.FavoriteBooks)
            .HasForeignKey(ufb => ufb.UserId);
        
        builder.HasOne(ufb => ufb.Book)
            .WithMany(b => b.FavoritedBy)
            .HasForeignKey(ufb => ufb.BookId);
    }
}