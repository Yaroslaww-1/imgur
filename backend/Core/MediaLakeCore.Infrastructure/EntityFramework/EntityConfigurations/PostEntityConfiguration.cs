using MediaLakeCore.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> entity)
        {
            entity.ToTable("post");

            entity
                .Property(c => c.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired();

            entity
                .Property(c => c.Content)
                .IsRequired();

            entity
                .Property(c => c.Content)
                .IsRequired();

            entity
                .HasOne(c => c.CreatedBy);
        }
    }
}
