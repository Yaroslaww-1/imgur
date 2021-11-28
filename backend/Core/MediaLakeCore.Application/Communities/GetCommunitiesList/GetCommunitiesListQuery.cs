using Dapper;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaLakeCore.Application.Communities.GetCommunitiesList
{
    public class GetCommunitiesListQuery : IRequest<IEnumerable<CommunitiesListItemDto>>
    {
    }

    internal class GetCommunitiesListQueryHandler : IRequestHandler<GetCommunitiesListQuery, IEnumerable<CommunitiesListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public GetCommunitiesListQueryHandler(MediaLakeCoreDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<CommunitiesListItemDto>> Handle(GetCommunitiesListQuery query, CancellationToken cancellationToken)
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
                        LEFT JOIN ""user"" u ON community.created_by_id = u.id;";

            var communities = await connection.QueryAsync<CommunitiesListItemDto, CommunitiesListItemCreatedByDto, CommunitiesListItemDto>(
                sql,
                (community, createdBy) => { community.CreatedBy = createdBy; return community; },
                splitOn: "Id,Id");

            return communities;
        }
    }
}
