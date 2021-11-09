using Ardalis.Specification;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Users.Specifications
{
    public class UserAggregateSpecification : Specification<User>
    {
        public UserAggregateSpecification()
        {
            base.Query.Include(u => u.Roles);
        }
    }
}
