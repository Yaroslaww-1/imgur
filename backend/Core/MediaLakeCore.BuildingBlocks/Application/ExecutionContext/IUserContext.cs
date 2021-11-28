using System;

namespace MediaLakeCore.BuildingBlocks.Application.ExecutionContext
{
    public interface IUserContext
    {
        public string Email { get; }
        public Guid UserId { get; }
    }
}
