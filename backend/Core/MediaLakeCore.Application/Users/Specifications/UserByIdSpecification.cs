using Ardalis.Specification;
using MediaLakeCore.Domain.Users;
using System;
using System.Linq;

namespace MediaLakeCore.Application.Users.Specifications
{
    public class UserByIdSpecification : Specification<User>, ISingleResultSpecification
    {
        public UserByIdSpecification(UserId userId)
        {
            base.Query.Where(u => u.Id == userId);
        }

        public UserByIdSpecification(Guid userId)
        {
            var _userId = new UserId(userId);
            base.Query.Where(u => u.Id == _userId);
        }
    }
}
