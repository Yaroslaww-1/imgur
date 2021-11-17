using Ardalis.Specification.EntityFrameworkCore;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediaLakeCore.Infrastructure.EventBus.Integration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Users.CreateUser
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
		public override string AggregateEntityName { get => nameof(User); }
	}

	public class UserCreatedIntegrationEventHandler : INotificationHandler<UserCreatedIntegrationEvent>
	{
        private readonly IUserRepository _userRepository;
        private readonly MediaLakeCoreDbContext _dbContext;

		public UserCreatedIntegrationEventHandler(IUserRepository userRepository, MediaLakeCoreDbContext dbContext)
		{
            _userRepository = userRepository;
            _dbContext = dbContext;
		}

        public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var roles = await _dbContext.Roles
                .WithSpecification(new RolesByNamesSpecification(notification.Roles))
                .ToListAsync();

            var user = User.Initialize(
                new UserId(notification.UserId),
                notification.Email,
                notification.Name,
                roles);

            await _userRepository.AddAsync(user);
        }
    }
}
