using Aplicacao.Service.ArquivosStorage;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
using Aplicacao.UseCase.UseCaseAsync;
using CloudinaryDotNet.Actions;
using Domain.Entidade.ProdutoEntidade;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;

public class ProdutoExcluirArquivosUseCase : UseCaseAsyncBase<ProdutoExcluirArquivosUseCaseInput, ProdutoExcluirArquivosUseCaseOutput>
{
    
    private readonly IProdutoRepositorio _produtoRepositorio;
    private readonly IArquivosStorageService _arquivosStorageService;
    
    public ProdutoExcluirArquivosUseCase(IProdutoRepositorio produtoRepositorio, IArquivosStorageService arquivosStorageService)
    {
        _produtoRepositorio = produtoRepositorio;
        _arquivosStorageService = arquivosStorageService;
    }
    
    protected override async Task<Result<ProdutoExcluirArquivosUseCaseOutput>> ExecuteUseCase(ProdutoExcluirArquivosUseCaseInput input)
    {
        var result = _produtoRepositorio.GetProdutoById(input.IdProduto);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        Produto produto = result.Value;
        
        foreach (var arquivo in input.ArquivoLista)
        {
            if (arquivo.EnumTipoArquivo == EnumTipoArquivo.Imagem)
            {
                var deleteArquivo = await _arquivosStorageService.DeleteArquivo(arquivo.PublicId, ResourceType.Image);

                if (deleteArquivo.IsFailed)
                    return Result.Fail(deleteArquivo.Errors);

                produto.ExcluirImagem(arquivo.PublicId);
            }
            else if (arquivo.EnumTipoArquivo == EnumTipoArquivo.Video)
            {
                var deleteArquivo = await _arquivosStorageService.DeleteArquivo(arquivo.PublicId, ResourceType.Video);

                if (deleteArquivo.IsFailed)
                    return Result.Fail(deleteArquivo.Errors);

                produto.ExcluirVideo(arquivo.PublicId);
            }
            else
                return Result.Fail("Erro em excluir a Url, tente novamente");
        }
        
        var atualizarDados = _produtoRepositorio.AtualizarProduto(produto);

        if (atualizarDados.IsFailed)
            return Result.Fail(atualizarDados.Errors);

        return Result.Ok(new ProdutoExcluirArquivosUseCaseOutput()
        {
            Mensagem = "Produto atualizado com sucesso!"
        });
    }
}