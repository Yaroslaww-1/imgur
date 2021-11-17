using MediaLakeCore.BuildingBlocks.ExecutionContext;

namespace MediaLakeCore.BuildingBlocks.Application.ExecutionContext
{
	public class UserContext : IUserContext
	{
		private readonly IExecutionContextAccessor _executionContextAccessor;

		public UserContext(IExecutionContextAccessor executionContextAccessor)
		{
			_executionContextAccessor = executionContextAccessor;
		}

		public string Email => new(_executionContextAccessor.Email);
    }
}
