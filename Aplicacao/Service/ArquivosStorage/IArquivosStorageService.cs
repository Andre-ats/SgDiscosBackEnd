using CloudinaryDotNet.Actions;
using FluentResults;

namespace Aplicacao.Service.ArquivosStorage;

public interface IArquivosStorageService
{
    public Task<Result<VideoUploadResult>> UploadVideoAsync(IFormFile arquivo);
    public Task<Result<ImageUploadResult>> UploadImageAsync(IFormFile arquivo);
    public Task<Result<DeletionResult>> DeleteArquivo(string publicId, ResourceType resourceType);
}