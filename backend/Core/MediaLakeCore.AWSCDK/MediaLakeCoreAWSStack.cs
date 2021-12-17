using Amazon.CDK;
using Amazon.CDK.AWS.S3;

namespace MediaLakeCore.AWSCDK
{
    public class MediaLakeCoreAWSStack : Stack
    {
        private readonly MediaLakeCoreAWSConfiguration _configuration;

        public MediaLakeCoreAWSStack(Construct parent, string id, IStackProps props, MediaLakeCoreAWSConfiguration configuration) : base(parent, id, props)
        {
            _configuration = configuration;

            new Bucket(this, $"{_configuration.GetS3BucketName()}-id", new BucketProps
            {
                Versioned = true,
                BucketName = _configuration.GetS3BucketName(),
                PublicReadAccess = true
            });
        }
    }
}
