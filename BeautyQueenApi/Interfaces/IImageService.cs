namespace BeautyQueenApi.Interfaces
{
    public interface IImageService
    {
        public Task<string> UploadImage(string path, IFormFile image);
        public void DeleteImage(string path, string imageName);
    }
}
