using Dapper;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;

namespace MediaLakeCore.Application.Communities.GetCommunitiesList
{
    public class GetCommunitiesListQuery : IRequest<IEnumerable<CommunitiesListItemDto>>
    {
    }

    internal class GetCommunitiesListQueryHandler : IRequestHandler<GetCommunitiesListQuery, IEnumerable<CommunitiesListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetCommunitiesListQueryHandler(MediaLakeCoreDbContext context, IUserContext userContext)
        {
            _dbContext = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CommunitiesListItemDto>> Handle(GetCommunitiesListQuery query, CancellationToken cancellationToken)
        {
            await using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        community.id AS {nameof(CommunitiesListItemDto.Id)},
                        community.name AS {nameof(CommunitiesListItemDto.Name)},
                        community.description AS {nameof(CommunitiesListItemDto.Description)},
                        (SELECT COUNT(*) FROM community_member WHERE community_member.community_id = community.id) AS {nameof(CommunitiesListItemDto.MembersCount)},
                        (CASE WHEN cm IS NOT NULL THEN True ELSE False END) AS {nameof(CommunitiesListItemDto.IsAuthenticatedUserJoined)},
                        u.id AS {nameof(CreatedByDto.Id)},
                        u.name AS {nameof(CreatedByDto.Name)}
                        FROM community
                        LEFT JOIN ""user"" u ON community.created_by_id = u.id
                        LEFT JOIN community_member cm ON community.id = cm.community_id AND cm.user_id = @AuthenticatedUserId;";
            
            var communities = await connection.QueryAsync<CommunitiesListItemDto, CreatedByDto, CommunitiesListItemDto>(
                sql,
                (community, createdBy) => { community.CreatedBy = createdBy; return community; },
                splitOn: "Id,Id",
                param: new
                {
                    AuthenticatedUserId = _userContext.UserId
                });

            return communities;
        }
    }
}