using AutoMapper;
using MediaLakeCore.Application.Users.Dtos;
using MediaLakeCore.Application.Users.Specifications;
using MediaLakeCore.Domain.Users;
using MediaLakeCore.Infrastructure.EntityFramework;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace MediaLakeCore.Application.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }

    internal class GetJobsQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly MediaLakeCoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetJobsQueryHandler(MediaLakeCoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .WithSpecification(new UserAggregateSpecification())
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
