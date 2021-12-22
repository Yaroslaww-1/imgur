using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MediaLakeCore.Application.Communities.GetCommunityById
{
    public class GetCommunityByIdQuery : IRequest<CommunityByIdDto>
    {
        public Guid CommunityId { get; set; }

        public GetCommunityByIdQuery(Guid communityId)
        {
            CommunityId = communityId;
        }
    }

    internal class GetCommunityByIdQueryHandler : IRequestHandler<GetCommunityByIdQuery, CommunityByIdDto>
    {
        private readonly MediaLakeCoreDbContext _dbContext;

        public GetCommunityByIdQueryHandler(MediaLakeCoreDbContext context)
        {
            _dbContext = context;
        }

        public async Task<CommunityByIdDto> Handle(GetCommunityByIdQuery query, CancellationToken cancellationToken)
        {
            await using var connection = _dbContext.Database.GetDbConnection();

            var sql = $@"SELECT
                        community.id AS {nameof(CommunityByIdDto.Id)},
                        community.name AS {nameof(CommunityByIdDto.Name)},
                        community.description AS {nameof(CommunityByIdDto.Description)},
                        (SELECT COUNT(*) FROM community_member WHERE community_member.community_id = community.id) AS {nameof(CommunityByIdDto.MembersCount)},
                        u.id AS {nameof(CommunityByIdDto.Id)},
                        u.name AS {nameof(CommunityByIdDto.Name)}
                        FROM community
                        LEFT JOIN ""user"" u ON community.created_by_id = u.id
                        WHERE community.id = @CommunityId;";

            var community = (await connection.QueryAsync<CommunityByIdDto, CreatedByDto, CommunityByIdDto>(
                sql,
                (community, createdBy) => { community.CreatedBy = createdBy; return community; },
                splitOn: "Id,Id",
                param: new
                {
                    query.CommunityId
                })).FirstOrDefault();

            return community;
        }
    }
}