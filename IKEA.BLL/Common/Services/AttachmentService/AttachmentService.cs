using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public readonly List<string> allowedExtension = new() { ".png", ".jpeg", "jgp" };
        public const int MaxSize = 2_097_152; // 2mb
        public  async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            // 1- Validate for Extension
            var extension = Path.GetExtension(file.FileName); //.png
            if (!allowedExtension.Contains(extension))
                return "Extension Not Valid";
            // 2- Validate for MaxSize
            if (file.Length > MaxSize)
                return "Size Must be Less Than or Equal 2mb";
            // 3- Get Located FolderPath
            //var folderPath = "C:\\Users\\OmarSaad\\source\\repos\\MVC_IKEA\\IKEA\\IKEA.PL\\wwwroot\\files\\Images\\"; //Static
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//files", folderName);
            // 4- Set Unique FileName
            var fileName = $"{Guid.NewGuid()}{extension}";
            // 5- Get FilePath
            var filePath = Path.Combine(folderPath, fileName);
            // 6- Save File as Stream[Data Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            //7- Copy File to FileStream
            await file.CopyToAsync(fileStream);
            // 8- Return FileName
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
