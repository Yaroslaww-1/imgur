using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaLakeUsers.Entities;

namespace MediaLakeUsers.Infrastructure.EntityFramework.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("role");

            entity
                .Property(u => u.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(u => u.Id);

            entity
                .Property(u => u.Name)
                .IsRequired();

            entity
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
