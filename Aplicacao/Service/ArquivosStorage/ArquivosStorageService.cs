using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentResults;

namespace Aplicacao.Service.ArquivosStorage;

public class ArquivosStorageService : IArquivosStorageService
{
    
    private readonly Cloudinary _cloudinary;

    public ArquivosStorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    
    public async Task<Result<ImageUploadResult>> UploadImageAsync(IFormFile arquivo)
    {

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(arquivo.FileName, arquivo.OpenReadStream()),
            Folder = "SgDiscos",
            UniqueFilename = false,
            Overwrite = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            return Result.Fail("Falha no upload da imagem.");

        return Result.Ok(result);
    }

    public async Task<Result<VideoUploadResult>> UploadVideoAsync(IFormFile arquivo)
    {

        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(arquivo.FileName, arquivo.OpenReadStream()),
            Folder = "SgDiscos",
            UniqueFilename = false,
            Overwrite = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            return Result.Fail("Falha no upload do vídeo.");

        return Result.Ok(result);
    }

    public async Task<Result<DeletionResult>> DeleteArquivo(string publicId, ResourceType resourceType)
    {
        var deleteParams = new DeletionParams(publicId)
        {
            ResourceType = resourceType
        };

        var result = await _cloudinary.DestroyAsync(deleteParams);
        
        if (result.Result != "ok")
            return Result.Fail("Não foi possível excluir o arquivo.");

        return  Result.Ok(result);
    }
    
}