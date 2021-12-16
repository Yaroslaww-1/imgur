using Ardalis.Specification.EntityFrameworkCore;
using MediaLakeCore.Application.Communities.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.CommunityMember;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Communities.LeaveCommunity
{
    public class LeaveCommunityCommand : IRequest<Unit>
    {
        public Guid CommunityId { get; set; }

        public LeaveCommunityCommand(Guid communityId)
        {
            CommunityId = communityId;
        }
    }

    internal class LeaveCommunityCommandHandler : IRequestHandler<LeaveCommunityCommand, Unit>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly ICommunityMemberRepository _communityMemberRepository;

        public LeaveCommunityCommandHandler(
            MediaLakeCoreDbContext dbContext,
            ICommunityRepository communityRepository,
            IUserContext userContext,
            IUserRepository userRepository,
            ICommunityMemberRepository communityMemberRepository)
        {
            _dbContext = dbContext;
            _communityRepository = communityRepository;
            _userContext = userContext;
            _userRepository = userRepository;
            _communityMemberRepository = communityMemberRepository;
        }

        public async Task<Unit> Handle(LeaveCommunityCommand command, CancellationToken cancellationToken)
        {
            var community = await _communityRepository.GetByIdAsync(new CommunityId(command.CommunityId));
            var user = await _userRepository.GetByIdAsync(new UserId(_userContext.UserId));

            var existingCommunityMember = await _dbContext.CommunityMembers
                .WithSpecification(new CommunityMemberByCommunityIdAndUserIdSpecification(community.Id, user.Id))
                .FirstOrDefaultAsync();

            if (existingCommunityMember == null)
            {
                throw new ArgumentException("User is not joined this community!"); //TODO: replace by custom exception
            }

            await _communityMemberRepository.DeleteAsync(existingCommunityMember);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
