using CloudinaryDotNet.Actions;
using FluentResults;

namespace Api.Service.ArquivosStorage;

public interface IArquivosStorageService
{
    public Task<Result<VideoUploadResult>> UploadVideoAsync(IFormFile arquivo);
    public Task<Result<ImageUploadResult>> UploadImageAsync(IFormFile arquivo);
}