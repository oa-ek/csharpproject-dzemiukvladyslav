using System.Drawing;
using System.Drawing.Imaging;

namespace BCS.API.FileService
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment environment;
        public FileService(IWebHostEnvironment env)
        {
            environment = env;
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Photos", imageFileName);
                if (File.Exists(dir))
                {
                    File.Delete(dir);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string SaveImage(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            try
            {
                using var stream = new MemoryStream(bytes);
                var image = System.Drawing.Image.FromStream(stream);
                var img = new Bitmap(image);
                string randomFilename = Path.GetRandomFileName() + Guid.NewGuid() + ".jpeg";
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "PhotoIMG", randomFilename);
                img.Save(dir, ImageFormat.Jpeg);
                return randomFilename;
            }
            catch
            {
                throw;
            }
        }

        public string SaveIFormFile(IFormFile photo)
        {
            try
            {
                var photoPath = Path.Combine(environment.ContentRootPath, "Photos");
                if (!Directory.Exists(photoPath))
                {
                    Directory.CreateDirectory(photoPath);
                }

                var uniqueFileName = Path.GetRandomFileName() + Guid.NewGuid() + Path.GetExtension(photo.FileName);

                var filePath = Path.Combine(photoPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                throw new Exception("Під час збереження файлу сталася помилка.", ex);
            }
        }
    }
}
