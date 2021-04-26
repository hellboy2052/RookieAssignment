using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services.Photo
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);

        Task<string> DeletePhoto(string publicId);
    }
}