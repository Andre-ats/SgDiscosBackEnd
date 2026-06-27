using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarLinks;

public class ProdutoAdicionarLinksUseCase : UseCaseBase<ProdutoAdicionarLinksUseCaseInput, ProdutoAdicionarLinksUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    
    public ProdutoAdicionarLinksUseCase(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }
    protected override Result<ProdutoAdicionarLinksUseCaseOutput> ExecuteUseCase(ProdutoAdicionarLinksUseCaseInput input)
    {

        var result = _produtoRepositorio.GetProdutoById(input.IdProduto);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        Produto produto = result.Value;

        foreach (var arquivoUrl in input.ArquivoUrlList)
        {
            if (input.TipoDoArquivo.Equals(EnumTipoArquivo.Imagem))
                produto.AdicionarImagem(arquivoUrl);
            else if (input.TipoDoArquivo.Equals(EnumTipoArquivo.Video))
                produto.AdicionarVideo(arquivoUrl);
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