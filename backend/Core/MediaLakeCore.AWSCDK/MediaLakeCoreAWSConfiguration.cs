namespace MediaLakeCore.AWSCDK
{
    public class MediaLakeCoreAWSConfiguration
    {
        private readonly AWSOptions _options;

        public MediaLakeCoreAWSConfiguration(AWSOptions options)
        {
            _options = options;
        }

        public string GetS3BucketName()
        {
            return $"media-lake-core-{_options.Environment}";
        }

        public string GetRegion()
        {
            return _options.Region;
        }
    }
}
