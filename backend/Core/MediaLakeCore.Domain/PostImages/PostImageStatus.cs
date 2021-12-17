using MediaLakeCore.BuildingBlocks.Domain;

namespace MediaLakeCore.Domain.PostImages
{
    public class PostImageStatus : EnumerationValueObject<PostImageStatus>
    {
		public static PostImageStatus Draft => new PostImageStatus(nameof(Draft));
		public static PostImageStatus UsedInCreatedPost => new PostImageStatus(nameof(UsedInCreatedPost));

		private PostImageStatus(string value)
			: base(value)
		{
		}
	}
}
