using Dapper;
using MediaLakeCore.BuildingBlocks.Application.ExecutionContext;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Communities.GetAuthenticatedUserCommunities
{
    public class GetAuthenticatedUserCommunitiesQuery : IRequest<IEnumerable<AuthenticatedUserCommunitiesListItemDto>>
    {
    }

    internal class GetAuthenticatedUserCommunitiesQueryHandler : IRequestHandler<GetAuthenticatedUserCommunitiesQuery, IEnumerable<AuthenticatedUserCommunitiesListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetAuthenticatedUserCommunitiesQueryHandler(MediaLakeCoreDbContext context, IUserContext userContext)
        {
            _dbContext = context;
            _userContext = userContext;
        }

        public async Task<IEnumerable<AuthenticatedUserCommunitiesListItemDto>> Handle(GetAuthenticatedUserCommunitiesQuery query, CancellationToken cancellationToken)
        {
            await using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        community.id AS {nameof(AuthenticatedUserCommunitiesListItemDto.Id)},
                        community.name AS {nameof(AuthenticatedUserCommunitiesListItemDto.Name)},
                        community.description AS {nameof(AuthenticatedUserCommunitiesListItemDto.Description)},
                        (SELECT COUNT(*) FROM community_member WHERE community_member.community_id = community.id) AS {nameof(AuthenticatedUserCommunitiesListItemDto.MembersCount)},
                        u.id AS {nameof(CreatedByDto.Id)},
                        u.name AS {nameof(CreatedByDto.Name)}
                        FROM community
                        LEFT JOIN ""user"" u ON community.created_by_id = u.id
                        LEFT JOIN community_member ON community_member.community_id = community.id
                        WHERE community_member.user_id = @UserId";

            var communities = await connection.QueryAsync<AuthenticatedUserCommunitiesListItemDto, CreatedByDto, AuthenticatedUserCommunitiesListItemDto>(
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
