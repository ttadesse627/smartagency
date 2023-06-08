

using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Formats;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }
        public bool UploadFormFile(IFormFile file, string fileName, string pathToSave, FileMode fileMode = FileMode.Create)
        {


            try
            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(pathToSave))
                    {
                        // If folder does not exist, create it
                        Directory.CreateDirectory(pathToSave);
                    }
                    var matchingFiles = Directory.GetFiles(pathToSave, fileName + "*");
                    //removing file with the same id but different extension 
                    matchingFiles.ToList().ForEach(file =>
                    {
                        //TODO: delete the file
                        System.IO.File.Delete(file);
                    });
                    var fileExtension = Path.GetExtension(file.FileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, fileMode))
                    {
                        file.CopyTo(stream);
                    }
                }
                return true;

            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public async Task<bool> UploadBase64FileAsync(string base64String, string fileName, string pathToSave, FileMode? fileMode = FileMode.Create)
        {


            try
            {
                if (!Directory.Exists(pathToSave))
                {
                    // If folder does not exist, create it
                    Directory.CreateDirectory(pathToSave);
                }

                // Convert the Base64 string to a byte array.
                string myString = base64String.Substring(base64String.IndexOf(',') + 1);
                //    _logger.LogCritical(myString);
                byte[] bytes = Convert.FromBase64String(myString);
                var extension = string.IsNullOrEmpty(getFileExtension(bytes)) ? "." + getFileExtension(bytes) : ".png";
                var fullPath = Path.Combine(pathToSave, fileName + extension);
                await File.WriteAllBytesAsync(fullPath, bytes);
                return true;

            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<bool> UploadBase64FilesAsync(IList<string> base64Strings, IList<Guid> fileNames, string pathToSave, FileMode? fileMode = FileMode.Create)
        {


            try
            {
                //    _logger.LogCritical(myString);
                var count = 0;
                foreach (var file in base64Strings)
                {
                    // exclude unwanted characters
                    // string myString = file.Substring(file.IndexOf(',') + 1);
                    // // Convert the Base64 string to a byte array.
                    // byte[] bytes = Convert.FromBase64String(file);
                    // var extension = string.IsNullOrEmpty(getFileExtension(bytes)) ? "." + getFileExtension(bytes) : ".png";
                    // var fullPath = Path.Combine(pathToSave, $"{fileNames[count++]}{extension}");
                    // await File.WriteAllBytesAsync(fullPath, bytes);
                    await UploadBase64FileAsync(file, fileNames[count++].ToString(), pathToSave, FileMode.Create);

                }
                return true;

            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public (byte[] file, string fileName, string fileExtenion) getFile(string fileId, string folder, string? folderType)
        {
            try
            {
                string folderName;
                if (folderType != null)
                {
                    folderName = Path.Combine("Resources", folder, folderType);
                }
                else
                {

                    folderName = Path.Combine("Resources", folder);
                }
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var matchingFiles = Directory.GetFiles(fullPath, fileId + "*");

                // if (matchingFiles.Length == 0)
                // {
                //     throw new NotFoundException("file not found");
                // }
                string actualFilePath = "";
                string fileExtension = "";
                string actualFileName = "";
                byte[] fileContent = new byte[] { };

                if (matchingFiles.Length != 0)
                {
                    actualFilePath = matchingFiles.First();
                    fileExtension = Path.GetExtension(actualFilePath);
                    actualFileName = Path.GetFileNameWithoutExtension(actualFilePath);

                    _logger.LogCritical(fileExtension);

                    fileContent = System.IO.File.ReadAllBytes(actualFilePath);
                }
                return (file: fileContent, fileName: actualFileName, fileExtenion: fileExtension);

            }
            catch (System.IO.DirectoryNotFoundException e)
            {

                throw new BadRequestException($"could not find the directory of the path specified:\n{e.Message}");
            }

        }

        public List<(byte[], string, string)> GetAllImages(string folderType)
        {
            try
            {
                string folderName = Path.Combine("Resources", folderType);
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var matchingFiles = Directory.GetFiles(fullPath);

                if (matchingFiles.Length == 0)
                {
                    throw new NotFoundException("no image found in the " + folderType + " folder");
                }

                List<(byte[], string, string)> Images = new List<(byte[], string, string)>();
                foreach (var filePath in matchingFiles)
                {
                    var fileExtension = Path.GetExtension(filePath);
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var fileContent = System.IO.File.ReadAllBytes(filePath);
                    Images.Add((fileContent, fileName, fileExtension));
                }

                return Images;

            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                throw new BadRequestException($"could not find the directory of the path specified:\n{e.Message}");
            }
        }


        public string DeleteFile(string fileName, string folderName)
        {
            //string folderPath = GetFolderPath(folderType);


            //return Path.Combine(basePath, folderPath);
            //string filePath = Path.Combine(folderPath, fileName);
            try
            {
                var folderNamee = Path.Combine("Resources", folderName);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderNamee);
                var matchingFiles = Directory.GetFiles(pathToSave, fileName);


                //removing file with the same id but different extension 
                matchingFiles.ToList().ForEach(file =>
                 {
                     //TODO: delete the file

                     System.IO.File.Delete(file);

                 });
                return "image deleted";
            }
            catch
            {
                return "failed to delete image";
            }




        }

        private string? getFileExtension(byte[] bytes)
        {
            // Use ImageSharp to identify the image format
            IImageFormat format = Image.DetectFormat(bytes);

            // Get the file extension from the image format
            return format?.FileExtensions.FirstOrDefault();

        }


    }
}
