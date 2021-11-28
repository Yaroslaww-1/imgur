using MediaLakeCore.Domain.PostReactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class PostReactionEntityConfiguration : IEntityTypeConfiguration<PostReaction>
    {
        public void Configure(EntityTypeBuilder<PostReaction> entity)
        {
            entity.ToTable("post_reaction");

            entity
                .Property(pr => pr.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(pr => pr.Id);

            entity
                .Property(pr => pr.IsLike)
                .IsRequired();

            entity
                .Property(pr => pr.PostId)
                .IsRequired();

            entity
                .Property(pr => pr.CreatedBy)
                .IsRequired();
        }
    }
}
