using Education.System.IServices.IApplicationService;
using Microsoft.AspNetCore.Http;

namespace Education.System.Services.ApplicationService;

public class StorageService : IStorageService
{
    private readonly string _folderName = "wwwroot/upload";

    public async Task<string> AddPhotoToStorage(IFormFile image)
    {
        // Ensure the _folderName directory exists
        if (image is null)
        {
            throw new Exception("Can't upload null");
        }
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), _folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a unique file name using GUID and retain the file extension
        var fileExtension = Path.GetExtension(image.FileName);
        var uniqueFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        try
        {
            // Check if a file with the same name exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Create a new local file and copy contents of the uploaded file
            await using var fs = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(fs);

            // Return the relative path
            var relativePath = Path.Combine(_folderName.Split("/")[1], uniqueFileName).Replace("\\", "/");
            return $"/{relativePath}";
        }
        catch (Exception ex)
        {
            // Handle exceptions (log it and/or rethrow it)
            // Log.Error(ex, "Error saving file to storage");
            throw new Exception("An error occurred while saving the file", ex);
        }
    }

    public async Task<string> AddVideoToStorage(IFormFile video)
    {
        // Ensure the _folderName directory exists
        if (video is null)
        {
            throw new Exception("Can't upload null");
        }
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), _folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a unique file name using GUID and retain the file extension
        var fileExtension = Path.GetExtension(video.FileName);
        var uniqueFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        try
        {
            // Check if a file with the same name exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Create a new local file and copy contents of the uploaded file
            await using var fs = new FileStream(filePath, FileMode.Create);
            await video.CopyToAsync(fs);

            // Return the relative path
            var relativePath = Path.Combine(_folderName.Split("/")[1], uniqueFileName).Replace("\\", "/");
            return $"/{relativePath}";
        }
        catch (Exception ex)
        {
            // Handle exceptions (log it and/or rethrow it)
            // Log.Error(ex, "Error saving file to storage");
            throw new Exception("An error occurred while saving the file", ex);
        }
    }

    public async Task<string> AddPdfToStorage(IFormFile pdf)
    {
        if (pdf is null)
        {
            throw new Exception("Can't upload null");
        }
        // Ensure the _folderName directory exists
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), _folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a unique file name using GUID and retain the file extension
        var fileExtension = Path.GetExtension(pdf.FileName);
        var uniqueFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        try
        {
            // Check if a file with the same name exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Create a new local file and copy contents of the uploaded file
            await using var fs = new FileStream(filePath, FileMode.Create);
            await pdf.CopyToAsync(fs);

            // Return the relative path
            var relativePath = Path.Combine(_folderName.Split("/")[1], uniqueFileName).Replace("\\", "/");
            return $"/{relativePath}";
        }
        catch (Exception ex)
        {
            // Handle exceptions (log it and/or rethrow it)
            // Log.Error(ex, "Error saving file to storage");
            throw new Exception("An error occurred while saving the file", ex);
        }
    }
    public Task<IFormFile> GetFile(string path)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFile(string? path)
    {
        try
        {
            if (string.IsNullOrEmpty(path))
            {
                return Task.FromResult(true);
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var filePath = Path.Combine(folderPath, path);
            if (Path.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}