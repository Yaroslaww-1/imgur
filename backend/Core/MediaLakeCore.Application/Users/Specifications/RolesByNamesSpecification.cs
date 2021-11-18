using Ardalis.Specification;
using MediaLakeCore.Domain.Users;
using System.Collections.Generic;

namespace MediaLakeCore.Application.Users.Specifications
{
    public class RolesByNamesSpecification : Specification<Role>, ISingleResultSpecification
    {
        public RolesByNamesSpecification(IList<string> rolesNames)
        {
            base.Query.Where(r => rolesNames.Contains(r.Name));
        }
    }
}
