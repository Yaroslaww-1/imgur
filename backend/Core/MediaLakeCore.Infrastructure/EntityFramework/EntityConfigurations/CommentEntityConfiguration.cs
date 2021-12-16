using MediaLakeCore.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.ToTable("comment");

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
                .Property(c => c.LikesCount)
                .IsRequired();

            entity
                .Property(c => c.DislikesCount)
                .IsRequired();

            entity
                .HasOne(cm => cm.CreatedBy);
        }
    }
}
