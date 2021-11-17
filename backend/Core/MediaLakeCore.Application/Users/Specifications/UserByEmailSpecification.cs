using Ardalis.Specification;
using MediaLakeCore.Domain.Users;

namespace MediaLakeCore.Application.Users.Specifications
{
    public class UserByEmailSpecification : Specification<User>, ISingleResultSpecification
    {
        public UserByEmailSpecification(string email)
        {
            base.Query.Where(u => u.Email == email);
        }
    }
}
