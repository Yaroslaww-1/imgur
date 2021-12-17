using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.PostImages;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Domain.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaLakeCore.Infrastructure.EntityFramework.Seeding
{
    public class DatabaseSeeder
    {
		private readonly IApplicationBuilder _app;

		public DatabaseSeeder(IApplicationBuilder app)
        {
			_app = app;
        }

		public void SeedDatabase()
		{
			SeedRoles();
			SeedUsers();
			SeedCommunities();
		}

		private void SeedRoles()
		{
			using (var scope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeCoreDbContext>();

				var existingRoles = context.Roles.ToList();

				if (!existingRoles.Any())
				{
					context.Roles.Add(Role.CreateNew("User"));

					context.Roles.Add(Role.CreateNew("Admin"));

					context.SaveChanges();
				}
			};
		}

		private void SeedUsers()
		{
			using (var scope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeCoreDbContext>();

				var existingUsers = context.Users.ToList();

				var existingRoles = context.Roles.ToList();

				if (!existingUsers.Any())
				{
					context.Users.Add(User.Initialize(
							new UserId(new Guid("22222222-2222-2222-2222-222222222222")),
							"user@gmail.com",
							"User",
							existingRoles.Where(r => r.Name == "User").ToList()));

					context.Users.Add(User.Initialize(
							new UserId(new Guid("11111111-1111-1111-1111-111111111111")),
							"admin@gmail.com",
							"Admin",
							existingRoles.Where(r => r.Name == "Admin").ToList()));

					context.SaveChanges();
				}
			};
		}

		private void SeedCommunities()
		{
			using (var scope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				using var context = scope.ServiceProvider.GetRequiredService<MediaLakeCoreDbContext>();

				var existingCommunities = context.Communities.ToList();

				if (!existingCommunities.Any())
				{
					var creator = context.Users.First(u => u.Id == new UserId(new Guid("22222222-2222-2222-2222-222222222222")));

					var community = Community.Initialize(
							new CommunityId(new Guid("11111111-1111-1111-1111-111111111111")),
							creator,
							"CSharp",
							"Community for C# developers!");

					context.Communities.Add(community);

					var post = new PostCreatorDomainService().CreateNewPost(
							community.Id,
							"C# 9.0 is released!",
							"A lot of new features.",
							creator,
							new List<PostImage>());

					context.Posts.Add(post);

					context.SaveChanges();
				}
			};
		}
	}
}
