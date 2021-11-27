using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Dapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.GetPostsList
{
    public class GetPostsListQuery : IRequest<IEnumerable<PostsListItemDto>>
    {
    }

    internal class GetPostsListQueryHandler : IRequestHandler<GetPostsListQuery, IEnumerable<PostsListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetPostsListQueryHandler(MediaLakeCoreDbContext context, IMapper mapper, IUserContext userContext)
        {
            _dbContext = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IEnumerable<PostsListItemDto>> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql =$@"SELECT
                        post.id AS {nameof(PostsListItemDto.Id)},
                        post.name AS {nameof(PostsListItemDto.Name)},
                        post.content AS {nameof(PostsListItemDto.Content)},
                        (SELECT COUNT(*) FROM post_comment WHERE post_comment.post_id = post.id) AS {nameof(PostsListItemDto.CommentsCount)},
                        (SELECT COUNT(*) FROM post_reaction post_reaction WHERE post_reaction.post_id = post.id AND is_like = TRUE) AS {nameof(PostsListItemDto.LikesCount)},
                        (SELECT COUNT(*) FROM post_reaction post_reaction WHERE post_reaction.post_id = post.id AND is_like = FALSE) AS {nameof(PostsListItemDto.DislikesCount)}
                        FROM post;";

            var posts = (await connection.QueryAsync<PostsListItemDto>(sql)).ToList();

            return posts;
        }
    }
}
