using System;

namespace MediaLakeCore.BuildingBlocks.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(Guid entityId) : base($"Entity with id {entityId} not found")
        {
        }
    }
}
