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
using Dapper;
using System.Linq;

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
            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        comment.id AS {nameof(CommentsListItemDto.Id)},
                        comment.content AS {nameof(CommentsListItemDto.Content)},
                        (SELECT COUNT(*) FROM comment_reaction WHERE comment_reaction.comment_id = comment.id AND is_like = TRUE) AS {nameof(CommentsListItemDto.LikesCount)},
                        (SELECT COUNT(*) FROM comment_reaction WHERE comment_reaction.comment_id = comment.id AND is_like = FALSE) AS {nameof(CommentsListItemDto.DislikesCount)},
                        u.id AS {nameof(CommentsListItemCreatedByDto.Id)},
                        u.name AS {nameof(CommentsListItemCreatedByDto.Name)}
                        FROM comment
                        LEFT JOIN ""user"" u ON comment.created_by_id = u.id
                        WHERE comment.post_id = @PostId;";

            var comments = await connection.QueryAsync<CommentsListItemDto, CommentsListItemCreatedByDto, CommentsListItemDto>(
                sql,
                (comment, createdBy) => { comment.CreatedBy = createdBy; return comment; },
                splitOn: "Id,Id",
                param: new
                {
                    PostId = query.PostId
                }
            );

            return comments;
        }
    }
}
