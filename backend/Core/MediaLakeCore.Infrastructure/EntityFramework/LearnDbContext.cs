using Microsoft.EntityFrameworkCore;
using MediaLakeCore.Infrastructure.EntityFramework.EntityConfigurations;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Comments;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.CommunityMember;
using MediaLakeCore.Domain.CommentReactions;
using MediaLakeCore.Domain.PostReactions;
using System.Threading;
using System.Threading.Tasks;
using MediaLakeCore.Infrastructure.EventBus.Domain;
using System.Linq;
using MediaLakeCore.BuildingBlocks.Domain;

namespace MediaLakeCore.Infrastructure.EntityFramework
{
    public class MediaLakeCoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<CommunityMember> CommunityMembers { get; set; }

        private readonly IDomainEventBus _domainEventBus;

        public MediaLakeCoreDbContext(IDomainEventBus domainEventBus) : base()
        {
            _domainEventBus = domainEventBus;
        }

        public MediaLakeCoreDbContext(
            DbContextOptions<MediaLakeCoreDbContext> options,
            IDomainEventBus domainEventBus
            ) : base(options)
        {
            _domainEventBus = domainEventBus;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleEntityConfiguration).Assembly);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var changedEntities = ChangeTracker.Entries()
                .ToList();

            await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            foreach (var changedEntity in changedEntities)
            {
                var entityType = changedEntity.Entity.GetType();
                if (entityType.GetProperty("DomainEvents") != null)
                {
                    await _domainEventBus.PublishAllAsync((changedEntity.Entity as Entity).DomainEvents);
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken); ;
        }
    }
}
