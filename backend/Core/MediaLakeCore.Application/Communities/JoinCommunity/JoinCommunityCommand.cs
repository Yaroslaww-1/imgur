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

namespace MediaLakeCore.Application.Communities.JoinCommunity
{
    public class JoinCommunityCommand : IRequest<Unit>
    {
        public Guid CommunityId { get; set; }

        public JoinCommunityCommand(Guid communityId)
        {
            CommunityId = communityId;
        }
    }

    internal class JoinCommunityCommandHandler : IRequestHandler<JoinCommunityCommand, Unit>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly ICommunityRepository _communityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly ICommunityMemberRepository _communityMemberRepository; 

        public JoinCommunityCommandHandler(
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

        public async Task<Unit> Handle(JoinCommunityCommand command, CancellationToken cancellationToken)
        {
            var community = await _communityRepository.GetByIdAsync(new CommunityId(command.CommunityId));
            var user = await _userRepository.GetByIdAsync(new UserId(_userContext.UserId));

            var existingCommunityMember = await _dbContext.CommunityMembers
                .WithSpecification(new CommunityMemberByCommunityIdAndUserIdSpecification(community.Id, user.Id))
                .FirstOrDefaultAsync();

            if (existingCommunityMember != null)
            {
                throw new ArgumentException("Already joined this community!"); //TODO: replace by custom exception
            }

            var member = CommunityMember.CreateNew(community.Id, user.Id);

            await _communityMemberRepository.AddAsync(member);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
