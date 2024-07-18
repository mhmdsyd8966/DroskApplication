using Microsoft.AspNetCore.Http;

namespace Education.System.IServices.IApplicationService;

public interface IStorageService
{
    Task<string> AddPhotoToStorage(IFormFile image);
    Task<string> AddVideoToStorage(IFormFile video);
    Task<string> AddPdfToStorage(IFormFile pdf);
    Task<IFormFile> GetFile(string path);
    Task<bool> DeleteFile(string? path);
}