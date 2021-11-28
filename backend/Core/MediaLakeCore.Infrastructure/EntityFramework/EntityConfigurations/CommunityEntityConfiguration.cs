using MediaLakeCore.Domain.Communities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations
{
    public class CommunityEntityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> entity)
        {
            entity.ToTable("community");

            entity
                .Property(c => c.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired();

            entity
                .Property(c => c.Description)
                .IsRequired();

            entity
                .HasOne(c => c.CreatedBy);
        }
    }
}
