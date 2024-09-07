using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GogoLibrary.DAL.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.RefreshToken).IsRequired();
        builder.Property(u => u.RefreshTokenExpiryTime).IsRequired();

        builder.HasOne(u => u.User)
            .WithOne(u => u.UserToken)
            .HasForeignKey<UserToken>(u => u.UserId);
    }
}