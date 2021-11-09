using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using MediaLakeUsers.Entities;

namespace MediaLakeUsers.Infrastructure.EntityFramework.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("user");

            entity
                .Property(u => u.Id)
                .HasColumnType("uuid")
                .IsRequired();

            entity.HasKey(u => u.Id);

            entity
                .Property(u => u.Name)
                .IsRequired();

            entity
                .Property(u => u.Email)
                .IsRequired();

            entity
                .HasIndex(u => u.Email)
                .IsUnique();

            entity
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "user_role",
                    u => u
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("role_id"),
                    r => r
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("user_id"));
        }
    }
}
