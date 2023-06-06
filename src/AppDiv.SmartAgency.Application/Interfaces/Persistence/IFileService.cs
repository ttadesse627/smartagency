using Microsoft.AspNetCore.Http;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IFileService
    {
       public bool UploadFormFile(IFormFile file, string fileName, string pathToSave , FileMode fileMode);
        public Task<bool> UploadBase64FileAsync(string base64String, string fileName, string pathToSave, FileMode? fileMode);
        public Task<bool> UploadBase64FilesAsync(IList<string> base64Strings, IList<Guid> fileName, string pathToSave, FileMode? fileMode);
        public (byte[]file,string fileName , string fileExtenion) getFile(string fileId, string folder, string? folderType);
        public List<(byte[], string, string)> GetAllImages(string folderType);
        public string DeleteFile(string fileName, string folderName);
    }
}
