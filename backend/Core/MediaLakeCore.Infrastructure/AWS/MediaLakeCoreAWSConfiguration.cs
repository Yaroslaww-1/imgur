using Amazon;
using MediaLakeCore.BuildingBlocks.Infrastructure.Options;
using Microsoft.Extensions.Configuration;

namespace MediaLakeCore.Infrastructure.AWS
{
    public class MediaLakeCoreAWSConfiguration
    {
        private readonly AWSOptions _options;

        public MediaLakeCoreAWSConfiguration(IConfiguration configuration)
        {
            _options = configuration.GetSection(AWSOptions.Location).Get<AWSOptions>();

            AWSConfigs.AWSRegion = _options.Region;
        }

        public string GetS3BucketName()
        {
            return $"media-lake-core-{_options.Environment}";
        }
    }
}
