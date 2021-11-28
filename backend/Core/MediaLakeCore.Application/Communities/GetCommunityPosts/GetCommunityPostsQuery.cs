using Dapper;
using MediaLakeCore.Application.Posts.GetPostsList;
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
    public class GetCommunityPostsQuery : IRequest<IEnumerable<PostsListItemDto>>
    {
        public Guid CommunityId { get; set; }

        public GetCommunityPostsQuery(Guid communityId)
        {
            CommunityId = communityId;
        }
    }

    internal class GetCommunityPostsQueryHandler : IRequestHandler<GetCommunityPostsQuery, IEnumerable<PostsListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public GetCommunityPostsQueryHandler(MediaLakeCoreDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<PostsListItemDto>> Handle(GetCommunityPostsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        post.id AS {nameof(PostsListItemDto.Id)},
                        post.name AS {nameof(PostsListItemDto.Name)},
                        post.content AS {nameof(PostsListItemDto.Content)},
                        (SELECT COUNT(*) FROM comment WHERE comment.post_id = post.id) AS {nameof(PostsListItemDto.CommentsCount)},
                        (SELECT COUNT(*) FROM post_reaction WHERE post_reaction.post_id = post.id AND is_like = TRUE) AS {nameof(PostsListItemDto.LikesCount)},
                        (SELECT COUNT(*) FROM post_reaction WHERE post_reaction.post_id = post.id AND is_like = FALSE) AS {nameof(PostsListItemDto.DislikesCount)}
                        FROM post
                        WHERE post.community_id = @CommunityId
                        ORDER BY post.created_at DESC;";

            var posts = (await connection.QueryAsync<PostsListItemDto>(
                sql,
                new
                {
                    CommunityId = request.CommunityId
                })
            ).ToList();

            return posts;
        }
    }
}
