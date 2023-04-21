using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Comon
{
    public class StorageService : IStorageService
    {
        private readonly string _userContentFolderPath;
        private const string USER_CONTENT_FOLDER_NAME = "AppData" ;

        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolderPath = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            string[] splitedPath = fileName.Split("/");
            var filePath = Path.Combine(_userContentFolderPath, splitedPath[splitedPath.Length-1]);
            if (File.Exists(filePath))
                await Task.Run(() => File.Delete(filePath));
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            if (!Directory.Exists(_userContentFolderPath))
                Directory.CreateDirectory(_userContentFolderPath);
            var filePath = Path.Combine(_userContentFolderPath, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        
    }
}
