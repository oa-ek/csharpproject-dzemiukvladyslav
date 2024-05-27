namespace BCS.API.FileService
{
    public interface IFileService
    {
        public string SaveImage(string imageFile);
        public bool DeleteImage(string imageFileName);
        public string SaveIFormFile(IFormFile photo);
    }
}
