using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GogoLibrary.DAL.Configurations;

public class UserClubConfiguration : IEntityTypeConfiguration<UserClub>
{
    public void Configure(EntityTypeBuilder<UserClub> builder)
    {
        //
    }
}