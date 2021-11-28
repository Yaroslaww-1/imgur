using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediaLakeCore.Application.Comments.Specifications;

namespace MediaLakeCore.Application.Comments.GetCommentsList
{
    public class GetCommentsListQuery : IRequest<IEnumerable<CommentsListItemDto>>
    {
        public Guid PostId { get; set; }

        public GetCommentsListQuery(Guid postId)
        {
            PostId = postId;
        }
    }

    internal class GetCommentsListQueryHandler : IRequestHandler<GetCommentsListQuery, IEnumerable<CommentsListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCommentsListQueryHandler(MediaLakeCoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentsListItemDto>> Handle(GetCommentsListQuery query, CancellationToken cancellationToken)
        {
            var comments = await _dbContext.Comments
                .WithSpecification(new CommentAggregateSpecification())
                .WithSpecification(new CommentsByPostIdSpecification(new PostId(query.PostId)))
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<CommentsListItemDto>>(comments);
        }
    }
}
