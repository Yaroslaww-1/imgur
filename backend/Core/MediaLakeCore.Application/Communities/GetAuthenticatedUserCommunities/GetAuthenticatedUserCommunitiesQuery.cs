using Dapper;
using MediaLakeCore.Application.Communities.GetCommunitiesList;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Communities.GetAuthenticatedUserCommunities
{
    public class GetAuthenticatedUserCommunitiesQuery : IRequest<IEnumerable<CommunitiesListItemDto>>
    {
    }

    internal class GetAuthenticatedUserCommunitiesQueryHandler : IRequestHandler<GetAuthenticatedUserCommunitiesQuery, IEnumerable<CommunitiesListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetAuthenticatedUserCommunitiesQueryHandler(MediaLakeCoreDbContext context, IUserContext userContext)
        {
            _dbContext = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CommunitiesListItemDto>> Handle(GetAuthenticatedUserCommunitiesQuery query, CancellationToken cancellationToken)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        community.id AS {nameof(CommunitiesListItemDto.Id)},
                        community.name AS {nameof(CommunitiesListItemDto.Name)},
                        community.description AS {nameof(CommunitiesListItemDto.Description)},
                        (SELECT COUNT(*) FROM community_member WHERE community_member.community_id = community.id) AS {nameof(CommunitiesListItemDto.MembersCount)},
                        u.id AS {nameof(CommunitiesListItemCreatedByDto.Id)},
                        u.name AS {nameof(CommunitiesListItemCreatedByDto.Name)}
                        FROM community
                        LEFT JOIN ""user"" u ON community.created_by_id = u.id
                        LEFT JOIN community_member ON community_member.community_id = community.id
                        WHERE community_member.user_id = @UserId";

            var communities = await connection.QueryAsync<CommunitiesListItemDto, CommunitiesListItemCreatedByDto, CommunitiesListItemDto>(
                sql,
                (community, createdBy) => { community.CreatedBy = createdBy; return community; },
                splitOn: "Id,Id",
                param: new {
                    UserId = _userContext.UserId
                });

            return communities;
        }
    }
}
