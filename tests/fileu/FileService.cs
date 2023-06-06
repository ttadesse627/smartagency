
using System;
using AppDiv.CRVS.Application.Exceptions;
using AppDiv.CRVS.Application.Interfaces;
using AppDiv.CRVS.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Formats;

namespace AppDiv.CRVS.Infrastructure.Services
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
        public (byte[] file, string fileName, string fileExtenion) getFile(string fileId, string folder)
        {

            var folderName = Path.Combine("Resources", folder);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var matchingFiles = Directory.GetFiles(fullPath, fileId + "*");

            if (matchingFiles.Length == 0)
            {
                throw new NotFoundException("file not found");

            }
            var actualFilePath = matchingFiles.First();
            var fileExtension = Path.GetExtension(actualFilePath);
            var actualFileName = Path.GetFileNameWithoutExtension(actualFilePath);

            _logger.LogCritical(fileExtension);

            var fileContent = System.IO.File.ReadAllBytes(actualFilePath);
            ;
            return (file: fileContent, fileName: actualFileName, fileExtenion: fileExtension);

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
