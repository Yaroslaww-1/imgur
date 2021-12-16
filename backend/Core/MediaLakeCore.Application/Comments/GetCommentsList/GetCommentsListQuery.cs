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
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;

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
        private readonly IUserContext _userContext;

        public GetCommentsListQueryHandler(MediaLakeCoreDbContext context, IUserContext userContext)
        {
            _dbContext = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CommentsListItemDto>> Handle(GetCommentsListQuery query, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        comment.id AS {nameof(CommentsListItemDto.Id)},
                        comment.content AS {nameof(CommentsListItemDto.Content)},
                        comment.likes_count AS {nameof(CommentsListItemDto.LikesCount)},
                        comment.dislikes_count AS {nameof(CommentsListItemDto.DislikesCount)},
                        u.id AS {nameof(CreatedByDto.Id)},
                        u.name AS {nameof(CreatedByDto.Name)},
                        cr.is_like AS {nameof(AuthenticatedUserReactionDto.IsLike)}
                        FROM comment
                        LEFT JOIN ""user"" u ON comment.created_by_id = u.id
                        LEFT JOIN comment_reaction cr ON cr.comment_id = comment.id AND cr.created_by = @AuthenticatedUserId
                        WHERE comment.post_id = @PostId;";

            var comments = await connection.QueryAsync<CommentsListItemDto, CreatedByDto, AuthenticatedUserReactionDto, CommentsListItemDto>(
                sql,
                (comment, createdBy, authenticatedUserReaction) =>
                {
                    comment.CreatedBy = createdBy;
                    comment.AuthenticatedUserReaction = authenticatedUserReaction;
                    return comment;
                },
                splitOn: "Id,Id,IsLike",
                param: new
                {
                    PostId = query.PostId,
                    AuthenticatedUserId = _userContext.UserId
                }
            );

            return comments;
        }
    }
}
