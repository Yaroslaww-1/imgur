using MediaLakeCore.Domain.PostImages;
using MediaLakeCore.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class PostImageEntityConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> entity)
        {
            entity.ToTable("post_image");

            entity
                .Property(c => c.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(c => c.Id);

            entity
                .Property(u => u.Url)
                .IsRequired();

            entity
               .HasOne<Post>()
               .WithMany()
               .HasForeignKey(c => c.PostId);

            entity.OwnsOne(pi => pi.Status, b =>
            {
                b.Property(p => p.Value).HasColumnName("status_code");
            });
        }
    }
}
