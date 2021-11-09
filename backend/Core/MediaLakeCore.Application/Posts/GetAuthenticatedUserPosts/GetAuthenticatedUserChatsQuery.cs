using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Chats.Specifications;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.GetChats
{
    public class GetAuthenticatedUserPostsQuery : IRequest<IEnumerable<PostDto>>
    {
    }

    internal class GetAuthenticatedUserPostsQueryHandler : IRequestHandler<GetAuthenticatedUserPostsQuery, IEnumerable<PostDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAuthenticatedUserPostsQueryHandler(MediaLakeCoreDbContext context, IMapper mapper, IUserContext userContext)
        {
            _dbContext = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetAuthenticatedUserPostsQuery request, CancellationToken cancellationToken)
        {
            var createdBy = await _dbContext.Users
                .WithSpecification(new UserByEmailSpecification(_userContext.Email))
                .AsNoTracking()
                .FirstAsync();

            var chats = await _dbContext.Posts
                .WithSpecification(new PostAggregateSpecification())
                .WithSpecification(new PostByCreatorIdSpecification(createdBy.Id))
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<PostDto>>(chats);
        }
    }
}
