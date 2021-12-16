using AutoMapper;
using Dapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Posts.Exceptions;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Posts.GetPostsById
{
    public class GetPostByIdQuery : IRequest<PostByIdDto>
    {
        public Guid PostId { get; set; }

        public GetPostByIdQuery(Guid postId)
        {
            PostId = postId;
        }
    }

    internal class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostByIdDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetPostByIdQueryHandler(MediaLakeCoreDbContext context, IMapper mapper, IUserContext userContext)
        {
            _dbContext = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<PostByIdDto> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        post.id AS {nameof(PostByIdDto.Id)},
                        post.name AS {nameof(PostByIdDto.Name)},
                        post.content AS {nameof(PostByIdDto.Content)},
                        post.comments_count AS {nameof(PostByIdDto.CommentsCount)},
                        post.likes_count AS {nameof(PostByIdDto.LikesCount)},
                        post.dislikes_count AS {nameof(PostByIdDto.DislikesCount)},
                        u.id AS {nameof(CreatedByDto.Id)},
                        u.name AS {nameof(CreatedByDto.Name)},
                        pr.is_like AS {nameof(AuthenticatedUserReactionDto.IsLike)}
                        FROM post
                        LEFT JOIN ""user"" u ON post.created_by_id = u.id
                        LEFT JOIN post_reaction pr ON pr.post_id = post.id AND pr.created_by = @AuthenticatedUserId
                        WHERE post.id = @PostId;";

            var posts = await connection.QueryAsync<PostByIdDto, CreatedByDto, AuthenticatedUserReactionDto, PostByIdDto>(
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
                    PostId = query.PostId,
                    AuthenticatedUserId = _userContext.UserId
                }
            );

            var post = posts.FirstOrDefault();

            if (post == null)
            {
                throw new PostNotFoundException(new PostId(query.PostId));
            }

            return post;
        }
    }
}
