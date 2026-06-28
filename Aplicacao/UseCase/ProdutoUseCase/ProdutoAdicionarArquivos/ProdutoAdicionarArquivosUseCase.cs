using Aplicacao.Service.ArquivosStorage;
using Aplicacao.UseCase.UseCaseAsync;
using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;

public class ProdutoAdicionarArquivosUseCase : UseCaseAsyncBase<ProdutoAdicionarArquivosUseCaseInput, ProdutoAdicionarArquivosUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    private readonly IArquivosStorageService _arquivosStorageService;
    
    public ProdutoAdicionarArquivosUseCase(IProdutoRepositorio produtoRepositorio, IArquivosStorageService arquivosStorageService)
    {
        _produtoRepositorio = produtoRepositorio;
        _arquivosStorageService = arquivosStorageService;
    }
    protected override async Task<Result<ProdutoAdicionarArquivosUseCaseOutput>> ExecuteUseCase(ProdutoAdicionarArquivosUseCaseInput input)
    {

        var result = _produtoRepositorio.GetProdutoById(input.IdProduto);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        Produto produto = result.Value;

        foreach (var arquivo in input.ArquivoAdicionarInputs)
        {

            if (arquivo.EnumTipoArquivo == EnumTipoArquivoProduto.Imagem)
            {
                var uploadImage = await _arquivosStorageService.UploadImageAsync(arquivo.Arquivo);
                
                if (uploadImage.IsFailed)
                    return Result.Fail(uploadImage.Errors);
                
                produto.AdicionarArquivo(new ArquivosProduto(uploadImage.Value.PublicId, EnumTipoArquivoProduto.Imagem));
            }
            else if (arquivo.EnumTipoArquivo == EnumTipoArquivoProduto.Video)
            {
                var uploadVideo = await _arquivosStorageService.UploadVideoAsync(arquivo.Arquivo);
                
                if (uploadVideo.IsFailed)
                    return Result.Fail(uploadVideo.Errors);
                
                produto.AdicionarArquivo(new ArquivosProduto(uploadVideo.Value.PublicId, EnumTipoArquivoProduto.Video));
            }
            else
                return Result.Fail("Erro em adicionar a Url, tente novamente");
        }

        var atualizarDados = _produtoRepositorio.AtualizarProduto(produto);

        if (atualizarDados.IsFailed)
            return Result.Fail(atualizarDados.Errors);
            
        return Result.Ok(new ProdutoAdicionarArquivosUseCaseOutput
        {
            Mensagem = "Produto atualizado com sucesso!"
        });
    }
}