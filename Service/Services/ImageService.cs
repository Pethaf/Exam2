using Microsoft.AspNetCore.Http;

namespace Service.Services
{
    public class ImageService
    {
        private static ImageService? instance = null;

        private ImageService()
        {
        }

        public static ImageService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageService();
                }
                return instance;
            }
        }

        private static string ImageBasePath
        {
            get
            {
                return Path.Join(Directory.GetCurrentDirectory(), "wwwroot", "images");
            }
        }

        public void Upload(IFormFile file) {
            var originalFileName = Path.GetFileName(file.FileName);
            var uniqueFilePath = Path.Combine(ImageBasePath, originalFileName);

            using (var stream = File.Create(uniqueFilePath))
            {
                file.CopyTo(stream);
            }
        }

        public List<string> GetAll()
        {
            var fileNames = new List<string>();
            string[] fileEntries = Directory.GetFiles(ImageBasePath);
            foreach (string filePath in fileEntries) { 
                
                var fileName = Path.GetFileName(filePath);
                if (!fileName.StartsWith('.'))  
                    fileNames.Add(fileName);
            }
            return fileNames;
        }

    }
}
