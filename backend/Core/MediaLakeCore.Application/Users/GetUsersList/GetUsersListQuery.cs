using AutoMapper;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaLakeCore.Application.Users.GetUsersList
{
    public class GetUsersListQuery : IRequest<IEnumerable<UsersListItemDto>>
    {
    }

    internal class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, IEnumerable<UsersListItemDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(MediaLakeCoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersListItemDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .WithSpecification(new UserAggregateSpecification())
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<UsersListItemDto>>(users);
        }
    }
}
