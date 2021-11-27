using System;

namespace MediaLakeCore.BuildingBlocks.ExecutionContext
{
    public interface IExecutionContextAccessor
    {
        public string Email { get; }
        public Guid UserId { get; }
    }
}
