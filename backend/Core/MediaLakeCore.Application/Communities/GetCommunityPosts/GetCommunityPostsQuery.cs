using Dapper;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Communities.GetCommunityPosts
{
    public class GetCommunityPostsQuery : IRequest<IEnumerable<CommunityPostsListItemDto>>
    {
        public Guid CommunityId { get; set; }

        public GetCommunityPostsQuery(Guid communityId)
        {
            CommunityId = communityId;
        }
    }

    internal class GetCommunityPostsQueryHandler : IRequestHandler<GetCommunityPostsQuery, IEnumerable<CommunityPostsListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetCommunityPostsQueryHandler(MediaLakeCoreDbContext context, IUserContext userContext)
        {
            _dbContext = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CommunityPostsListItemDto>> Handle(GetCommunityPostsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        post.id AS {nameof(CommunityPostsListItemDto.Id)},
                        post.name AS {nameof(CommunityPostsListItemDto.Name)},
                        post.content AS {nameof(CommunityPostsListItemDto.Content)},
                        post.comments_count AS {nameof(CommunityPostsListItemDto.CommentsCount)},
                        post.likes_count AS {nameof(CommunityPostsListItemDto.LikesCount)},
                        post.dislikes_count AS {nameof(CommunityPostsListItemDto.DislikesCount)},
                        u.id AS {nameof(CreatedByDto.Id)},
                        u.name AS {nameof(CreatedByDto.Name)},
                        pr.is_like AS {nameof(AuthenticatedUserReactionDto.IsLike)},
                        pi.url AS {nameof(CommunityPostsListItemDto.ImagesUrls)}
                        FROM post
                        LEFT JOIN ""user"" u ON post.created_by_id = u.id
                        LEFT JOIN post_reaction pr ON pr.post_id = post.id AND pr.created_by = @AuthenticatedUserId
                        LEFT JOIN post_image pi ON pi.post_id = post.id
                        WHERE post.community_id = @CommunityId
                        ORDER BY post.created_at DESC;";

            var posts = await connection.QueryAsync<CommunityPostsListItemDto, CreatedByDto, AuthenticatedUserReactionDto, string, CommunityPostsListItemDto>(
               sql,
               (post, createdBy, authenticatedUserReaction, imageUrl) =>
               {
                   post.CreatedBy = createdBy;
                   post.AuthenticatedUserReaction = authenticatedUserReaction;
                   post.ImagesUrls.Add(imageUrl);
                   return post;
               },
               splitOn: "Id,Id,IsLike,ImagesUrls",
               param: new
               {
                   AuthenticatedUserId = _userContext.UserId,
                   CommunityId = request.CommunityId
               }
           );

            return posts;
        }
    }
}
