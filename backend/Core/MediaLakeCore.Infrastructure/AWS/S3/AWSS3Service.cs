using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MediaLakeCore.BuildingBlocks.Application;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.AWS.S3
{
    public class AWSS3Service : IFileService
    {
        private readonly MediaLakeCoreAWSConfiguration _awsConfiguration;
        private readonly AmazonS3Client _amazonS3Client;

        public AWSS3Service(MediaLakeCoreAWSConfiguration awsConfiguration)
        {
            _awsConfiguration = awsConfiguration;

            _amazonS3Client = new AmazonS3Client(RegionEndpoint.GetBySystemName(_awsConfiguration.GetRegion()));
        }

        public async Task<string> UploadPublicFileAsync(string fileContentBase64)
        {
            var fileKey = Guid.NewGuid().ToString();

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = new MemoryStream(Convert.FromBase64String(fileContentBase64)),
                Key = fileKey,
                BucketName = _awsConfiguration.GetS3BucketName()
            };

            using var transferUtility = new TransferUtility(_amazonS3Client);

            await transferUtility.UploadAsync(uploadRequest);

            return GetPublicUrl(fileKey);
        }

        public string GetPublicUrl(string key)
        {
            return $"http://{_awsConfiguration.GetS3BucketName()}.s3.amazonaws.com/{key}";
        }

        public async Task DeletePublicFileByUrlAsync(string fileUrl)
        {
            var key = fileUrl.Split("s3.amazonaws.com/").Last();

            var deleteRequest = new DeleteObjectRequest
            {
                Key = key,
                BucketName = _awsConfiguration.GetS3BucketName()
            };

            await _amazonS3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}
