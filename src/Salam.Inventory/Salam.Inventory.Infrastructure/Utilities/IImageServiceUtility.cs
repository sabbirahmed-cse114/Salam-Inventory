using Microsoft.AspNetCore.Http;

namespace Salam.Inventory.Infrastructure.Utilities
{
    public interface IImageServiceUtility
    {
        Task<string?> UploadImage(IFormFile? Picture);
        Task DeleteImage(string? imagePath);
    }
}