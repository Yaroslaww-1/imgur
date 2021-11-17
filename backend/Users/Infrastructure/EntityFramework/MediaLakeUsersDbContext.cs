using Microsoft.EntityFrameworkCore;
using MediaLakeUsers.Entities;
using MediaLakeUsers.Infrastructure.EntityFramework.EntityConfigurations;

namespace MediaLakeUsers.Infrastructure.EntityFramework
{
    public class MediaLakeUsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public MediaLakeUsersDbContext(DbContextOptions<MediaLakeUsersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleEntityConfiguration).Assembly);
        }
    }
}
