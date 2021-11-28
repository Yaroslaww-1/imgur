using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.CommunityMember;
using MediaLakeCore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class CommunityMemberEntityConfiguration : IEntityTypeConfiguration<CommunityMember>
    {
        public void Configure(EntityTypeBuilder<CommunityMember> entity)
        {
            entity.ToTable("community_member");

            entity
                .Property(c => c.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(c => c.Id);

            entity
                .HasOne<Community>()
                .WithMany()
                .HasForeignKey(c => c.CommunityId);

            entity
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
        }
    }
}
