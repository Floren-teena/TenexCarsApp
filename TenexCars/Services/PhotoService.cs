using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using TenexCars.DataAccess;
using TenexCars.Interfaces;
using TenexCars.Helper;

namespace TenexCars.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ApplicationDbContext _context;

        public PhotoService(IOptions<CloudinarySettings> config, ApplicationDbContext context)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
            _context = context;


        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream(); // Read in streams

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);


            }

            return uploadResult;

        }
        public async Task<ImageUploadResult> UpdatePhotoAsync(string id, IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null");
            }

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = id// specify the public ID of the existing image to be replaced
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        public async Task<RawUploadResult> AddDocumentAsync(IFormFile doc)
        {
            var uploadResult = new RawUploadResult(); //method for uploading document
            if (doc.Length > 0) //Checks if there is at least 1 file
            {
                using var stream = doc.OpenReadStream(); //Reads the file

                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(doc.FileName, stream),
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);  //uploads the file to cloudinary
            }

            return uploadResult;
        }


        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }

    }
}
