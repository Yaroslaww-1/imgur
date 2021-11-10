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
    public class GetPostsListQuery : IRequest<IEnumerable<PostForListDto>>
    {
    }

    internal class GetPostsListQueryHandler : IRequestHandler<GetPostsListQuery, IEnumerable<PostForListDto>>
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

        public async Task<IEnumerable<PostForListDto>> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql =$@"SELECT
                        post.id AS {nameof(PostForListDto.Id)},
                        post.name AS {nameof(PostForListDto.Name)},
                        post.content AS {nameof(PostForListDto.Content)},
                        (SELECT COUNT(*) FROM post_comment WHERE post_comment.post_id = post.id) AS {nameof(PostForListDto.CommentsCount)}
                        FROM post;";

            var posts = (await connection.QueryAsync<PostForListDto>(sql)).ToList();

            return posts;
        }
    }
}
