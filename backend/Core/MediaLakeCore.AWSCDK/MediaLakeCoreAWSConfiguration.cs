namespace MediaLakeCore.AWSCDK
{
    public class MediaLakeCoreAWSConfiguration
    {
        private readonly string _environment;

        public MediaLakeCoreAWSConfiguration()
        {
            _environment = System.Environment.GetEnvironmentVariable("AWSOptions__Environment");
        }

        public string GetS3BucketName()
        {
            return $"media-lake-core-{_environment}";
        }
    }
}
