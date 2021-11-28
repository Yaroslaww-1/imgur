using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Dapper;
using MediaLakeCore.Application.Posts.Dtos;
using MediaLakeCore.Application.Posts.Exceptions;
using MediaLakeCore.Application.Posts.Specifications;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Domain.Posts;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                        (SELECT COUNT(*) FROM comment WHERE comment.post_id = post.id) AS {nameof(PostByIdDto.CommentsCount)},
                        (SELECT COUNT(*) FROM post_reaction WHERE post_reaction.post_id = @PostId AND is_like = TRUE) AS {nameof(PostByIdDto.LikesCount)},
                        (SELECT COUNT(*) FROM post_reaction WHERE post_reaction.post_id = @PostId AND is_like = FALSE) AS {nameof(PostByIdDto.DislikesCount)},
                        u.id AS {nameof(PostByIdCreatedByDto.Id)},
                        u.name AS {nameof(PostByIdCreatedByDto.Name)}
                        FROM post
                        LEFT JOIN ""user"" u ON post.created_by_id = u.id
                        WHERE post.id = @PostId;";

            var posts = await connection.QueryAsync<PostByIdDto, PostByIdCreatedByDto, PostByIdDto>(
                sql,
                (post, createdBy) => { post.CreatedBy = createdBy; return post; },
                splitOn: "Id,Id",
                param: new
                {
                    PostId = query.PostId
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
