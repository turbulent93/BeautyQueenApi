using BeautyQueenApi.Interfaces;
using System.Xml.Linq;

namespace BeautyQueenApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public ImageService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<string> UploadImage(string uploadPath, IFormFile image)
        {
            string fileName = Guid.NewGuid() + "." + image.FileName.Split(".")[1];
            string path = uploadPath + "/" + fileName;

            using var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
            await image.CopyToAsync(fileStream);

            return fileName;
        }

        public void DeleteImage(string deletePath, string imageName)
        {
            var path = _appEnvironment.WebRootPath + "/files/gallery/" + imageName;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

    }
}
