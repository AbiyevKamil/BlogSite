using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Core.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Web.Helpers
{
    public class FileService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile file, UploadFileType uploadFileType)
        {
            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string corePath = "/uploads/blogs";
                
                switch (uploadFileType)
                {
                    case UploadFileType.Banner:
                        corePath = "/uploads/blogs";
                        break;
                    case UploadFileType.ProfilePicture:
                        corePath = "/uploads/users";
                        break;
                }

                string path = filename + Guid.NewGuid() + extension;

                string filePath = Path.Combine(wwwRootPath + corePath, path);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                return path;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}