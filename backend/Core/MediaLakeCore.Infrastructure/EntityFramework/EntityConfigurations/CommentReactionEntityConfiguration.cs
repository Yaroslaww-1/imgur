using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class CommentReactionEntityConfiguration : IEntityTypeConfiguration<CommentReaction>
    {
        public void Configure(EntityTypeBuilder<CommentReaction> entity)
        {
            entity.ToTable("comment_reaction");

            entity
                .Property(pr => pr.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(pr => pr.Id);

            entity
                .Property(pr => pr.IsLike)
                .IsRequired();

            entity
                .HasOne<Comment>()
                .WithMany()
                .HasForeignKey(pr => pr.CommentId)
                .IsRequired();

            entity
                .Property(pr => pr.CreatedBy)
                .IsRequired();
        }
    }
}
