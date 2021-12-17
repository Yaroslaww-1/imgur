using System.Threading.Tasks;

namespace MediaLakeCore.BuildingBlocks.Application
{
    public interface IFileService
    {
        public Task<string> UploadPublicFileAsync(string fileContentBase64);
        public Task DeletePublicFileByUrlAsync(string fileUrl);
    }
}
