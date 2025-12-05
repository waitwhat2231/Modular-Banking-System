using Microsoft.AspNetCore.Http;

namespace Modules.Users.Domain.Services
{
    public interface IFileService
    {
        Task<List<string>> SaveFilesAsync(List<IFormFile> file, string path, string[] allowedFileExtensions);
        string SaveFile(IFormFile file, string path, string[] allowedFileExtensions);
        Task<string> SaveFileAsync(IFormFile file, string path, string[] allowedFileExtensions);
        void DeleteFile(string fileNameWithExtension);
        Task<string> SaveBytesImage(byte[] bytes, string path);
    }
}
