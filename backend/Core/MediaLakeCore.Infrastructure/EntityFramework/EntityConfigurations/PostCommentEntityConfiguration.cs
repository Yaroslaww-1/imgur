using MediaLakeCore.Domain.PostComments;
using MediaLakeCore.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class PostCommentEntityConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> entity)
        {
            entity.ToTable("post_comment");

            entity
                .Property(cm => cm.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(cm => cm.Id);

            entity
                .Property(c => c.Content)
                .IsRequired();

            entity
                .Property(c => c.PostId)
                .IsRequired();

            entity
                .HasOne(cm => cm.CreatedBy);
        }
    }
}
