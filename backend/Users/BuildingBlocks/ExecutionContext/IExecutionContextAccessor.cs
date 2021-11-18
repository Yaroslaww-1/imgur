using System;

namespace MediaLakeUsers.BuildingBlocks.ExecutionContext
{
    public interface IExecutionContextAccessor
    {
        string Email { get; }
    }
}
