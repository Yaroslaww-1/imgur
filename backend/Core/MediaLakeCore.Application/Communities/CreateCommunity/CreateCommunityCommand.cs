using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Communities.CreateCommunity
{
    public class CreateCommunityCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateCommunityCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    internal class CreateCommunityCommandHandler : IRequestHandler<CreateCommunityCommand, Guid>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserContext _userContext;

        public CreateCommunityCommandHandler(MediaLakeCoreDbContext dbContext, ICommunityRepository communityRepository, IUserContext userContext)
        {
            _dbContext = dbContext;
            _communityRepository = communityRepository;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _dbContext.Users
                .WithSpecification(new UserAggregateSpecification())
                .WithSpecification(new UserByIdSpecification(_userContext.UserId))
                .FirstAsync();

            var community = Community.CreateNew(
                createdBy,
                request.Name,
                request.Description);

            await _communityRepository.AddAsync(community);

            return community.Id.Value;
        }
    }
}
