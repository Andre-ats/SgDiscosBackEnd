using Aplicacao.Service.ArquivosStorage;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
using Aplicacao.UseCase.UseCaseAsync;
using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarLinks;

public class ProdutoAdicionarLinksUseCase : UseCaseAsyncBase<ProdutoAdicionarLinksUseCaseInput, ProdutoAdicionarLinksUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    private readonly IArquivosStorageService _arquivosStorageService;
    
    public ProdutoAdicionarLinksUseCase(IProdutoRepositorio produtoRepositorio, IArquivosStorageService arquivosStorageService)
    {
        _produtoRepositorio = produtoRepositorio;
        _arquivosStorageService = arquivosStorageService;
    }
    protected override async Task<Result<ProdutoAdicionarLinksUseCaseOutput>> ExecuteUseCase(ProdutoAdicionarLinksUseCaseInput input)
    {

        var result = _produtoRepositorio.GetProdutoById(input.IdProduto);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        Produto produto = result.Value;

        foreach (var arquivoUrl in input.ArquivoLista)
        {

            if (input.TipoDoArquivo == EnumTipoArquivo.Imagem)
            {
                var uploadImage = await _arquivosStorageService.UploadImageAsync(arquivoUrl);
                
                if (uploadImage.IsFailed)
                    return Result.Fail(uploadImage.Errors);
                
                produto.AdicionarImagem(uploadImage.Value.PublicId);
            }
            else if (input.TipoDoArquivo == EnumTipoArquivo.Video)
            {
                var uploadVideo = await _arquivosStorageService.UploadVideoAsync(arquivoUrl);
                
                if (uploadVideo.IsFailed)
                    return Result.Fail(uploadVideo.Errors);
                
                produto.AdicionarVideo(uploadVideo.Value.PublicId);
            }
            else
                return Result.Fail("Erro em adicionar a Url, tente novamente");
        }

        var atualizarDados = _produtoRepositorio.AtualizarProduto(produto);

        if (atualizarDados.IsFailed)
            return Result.Fail(atualizarDados.Errors);
            
        return Result.Ok(new ProdutoAdicionarLinksUseCaseOutput
        {
            Mensagem = "Produto atualizado com sucesso!"
        });
    }
}