using Microsoft.EntityFrameworkCore;
using MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.PostReactions;
using MediaLakeCore.Domain.Comments;

namespace MediaLakeCore.Infrastructure.EntityFramework
{
    public class MediaLakeCoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }

        public MediaLakeCoreDbContext() : base() { }

        public MediaLakeCoreDbContext(DbContextOptions<MediaLakeCoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleEntityConfiguration).Assembly);
        }
    }
}
