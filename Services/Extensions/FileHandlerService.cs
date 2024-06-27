

using System.Net.Http.Headers;

namespace NestAlbania.Services.Extensions
{
    public class FileHandlerService : IFileHandlerService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly List<string> _permittedExtensions = new List<string> { ".pdf", ".jpg", ".jpeg", ".png", ".gif" };
        private readonly Dictionary<string, string> _mimeTypes = new Dictionary<string, string>
        {
            { ".pdf", "application/pdf" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" }
        };

        public FileHandlerService(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }
        public async Task<List<string>> UploadAsync(IFormFileCollection files, string uploadDir)
        {
            List<string> imagesName = new List<string>();

            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, uploadDir);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        Console.WriteLine(fileName);

                        // Generate a unique file name to avoid overwriting existing files
                        var uniqueFileName = GetUniqueFileName(fileName, uploads);

                        using (var fileStream = new FileStream(Path.Combine(uploads, uniqueFileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            imagesName.Add(uniqueFileName);
                        }
                    }
                }
            }
            return imagesName;
        }

        private string GetUniqueFileName(string fileName, string uploadDir)
        {
            // Create a unique file name by appending a GUID to the file name
            var uniqueFileName = Path.GetFileNameWithoutExtension(fileName)
                                 + "_" + Guid.NewGuid().ToString().Substring(0, 8)
                                 + Path.GetExtension(fileName);

            // Check if the file already exists, if so, generate a new unique file name
            while (File.Exists(Path.Combine(uploadDir, uniqueFileName)))
            {
                uniqueFileName = Path.GetFileNameWithoutExtension(fileName)
                                 + "_" + Guid.NewGuid().ToString().Substring(0, 8)
                                 + Path.GetExtension(fileName);
            }

            return uniqueFileName;
        }


        public async Task<string> UploadAndRenameFileAsync(IFormFile file, string uploadDir, string fileName)
        {
            var usedFileName = default(string);
            if (file != null)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, uploadDir);
                var _fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                _fileName = fileName + Path.GetExtension(_fileName);
                Console.WriteLine(_fileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, _fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    usedFileName = _fileName;
                }
            }
            return usedFileName;
        }
       

        public void RemoveImageFile(string imageDir, string imgName)
        {
            if (imgName != null)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, imageDir);
                var fileDel = Path.Combine(uploads, imgName);
                if (File.Exists(fileDel))
                {
                    File.Delete(fileDel);
                }
            }
        }
    }
}
