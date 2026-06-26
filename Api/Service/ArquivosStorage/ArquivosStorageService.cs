using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentResults;

namespace Api.Service.ArquivosStorage;

public class ArquivosStorageService : IArquivosStorageService
{
    
    private readonly Cloudinary _cloudinary;

    public ArquivosStorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    
    public async Task<Result<VideoUploadResult>> UploadVideoAsync(IFormFile arquivo)
    {
        var uploadParams = new VideoUploadParams()
        {
            File = new FileDescription(arquivo.FileName, arquivo.OpenReadStream()),
            Folder = "SgDiscos"
        };
        
        var result = await _cloudinary.UploadAsync(uploadParams);
        
        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            return Result.Fail("Falha no upload do video.");

        return Result.Ok(result);
    }

    public async Task<Result<ImageUploadResult>> UploadImageAsync(IFormFile arquivo)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(arquivo.FileName, arquivo.OpenReadStream()),
            Folder = "SgDiscos"
        };
        
        var result = await _cloudinary.UploadAsync(uploadParams);
        
        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            return Result.Fail("Falha no upload da imagem.");

        return Result.Ok(result);
    }
}