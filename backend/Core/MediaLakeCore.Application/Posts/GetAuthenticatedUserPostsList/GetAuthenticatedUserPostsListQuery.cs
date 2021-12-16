using AutoMapper;
using Dapper;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.GetAuthenticatedUserPostsList
{
    public class GetAuthenticatedUserPostsListQuery : IRequest<IEnumerable<AuthenticatedUserPostsListItemDto>>
    {
    }

    internal class GetAuthenticatedUserPostsListQueryHandler : IRequestHandler<GetAuthenticatedUserPostsListQuery, IEnumerable<AuthenticatedUserPostsListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAuthenticatedUserPostsListQueryHandler(MediaLakeCoreDbContext context, IMapper mapper, IUserContext userContext)
        {
            _dbContext = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<AuthenticatedUserPostsListItemDto>> Handle(GetAuthenticatedUserPostsListQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql =$@"SELECT
                        post.id AS {nameof(AuthenticatedUserPostsListItemDto.Id)},
                        post.name AS {nameof(AuthenticatedUserPostsListItemDto.Name)},
                        post.content AS {nameof(AuthenticatedUserPostsListItemDto.Content)},
                        post.comments_count AS {nameof(AuthenticatedUserPostsListItemDto.CommentsCount)},
                        post.likes_count AS {nameof(AuthenticatedUserPostsListItemDto.LikesCount)},
                        post.dislikes_count AS {nameof(AuthenticatedUserPostsListItemDto.DislikesCount)},
                        u.id AS {nameof(CreatedByDto.Id)},
                        u.name AS {nameof(CreatedByDto.Name)},
                        pr.is_like AS {nameof(AuthenticatedUserReactionDto.IsLike)}
                        FROM post
                        LEFT JOIN community_member ON community_member.community_id = post.community_id
                        LEFT JOIN ""user"" u ON post.created_by_id = u.id
                        LEFT JOIN post_reaction pr ON pr.post_id = post.id AND pr.created_by = @AuthenticatedUserId
                        WHERE community_member.user_id = @AuthenticatedUserId
                        ORDER BY post.created_at DESC;";

            var posts = await connection.QueryAsync<AuthenticatedUserPostsListItemDto, CreatedByDto, AuthenticatedUserReactionDto, AuthenticatedUserPostsListItemDto>(
               sql,
               (post, createdBy, authenticatedUserReaction) =>
               {
                   post.CreatedBy = createdBy;
                   post.AuthenticatedUserReaction = authenticatedUserReaction;
                   return post;
               },
               splitOn: "Id,Id,IsLike",
               param: new
               {
                   AuthenticatedUserId = _userContext.UserId
               }
           );

            return posts;
        }
    }
}
