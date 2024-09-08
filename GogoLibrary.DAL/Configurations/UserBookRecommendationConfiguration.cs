using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GogoLibrary.DAL.Configurations;

public class UserBookRecommendationConfiguration : IEntityTypeConfiguration<UserBookRecommendation>
{
    public void Configure(EntityTypeBuilder<UserBookRecommendation> builder)
    {
        builder.HasKey(ufb => new { ufb.UserId, ufb.BookId });
        
        builder.HasOne(ufb => ufb.User)
            .WithMany(u => u.UserBookRecommendations)
            .HasForeignKey(ufb => ufb.UserId);
        
        builder.HasOne(ufb => ufb.Book)
            .WithMany(b => b.RecommendedBy)
            .HasForeignKey(ufb => ufb.BookId);
    }
}