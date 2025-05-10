using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.AttachmentService
{
    public interface IAttachmentService 
    {
        public Task<string?> UploadAsync(IFormFile file, string folderName);
        public bool Delete(string filePath);
    }
}
