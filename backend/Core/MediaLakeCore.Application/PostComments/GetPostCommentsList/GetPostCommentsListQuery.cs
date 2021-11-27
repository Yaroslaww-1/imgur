using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Application.PostComments.Specifications;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaLakeCore.Application.PostComments.GetPostCommentsList
{
    public class GetPostCommentsListQuery : IRequest<IEnumerable<PostCommentsListItemDto>>
    {
        public Guid PostId { get; set; }

        public GetPostCommentsListQuery(Guid postId)
        {
            PostId = postId;
        }
    }

    internal class GetPostCommentsListQueryHandler : IRequestHandler<GetPostCommentsListQuery, IEnumerable<PostCommentsListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostCommentsListQueryHandler(MediaLakeCoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostCommentsListItemDto>> Handle(GetPostCommentsListQuery query, CancellationToken cancellationToken)
        {
            var comments = await _dbContext.PostComments
                .WithSpecification(new PostCommentAggregateSpecification())
                .WithSpecification(new CommentsByPostIdSpecification(new PostId(query.PostId)))
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<PostCommentsListItemDto>>(comments);
        }
    }
}
