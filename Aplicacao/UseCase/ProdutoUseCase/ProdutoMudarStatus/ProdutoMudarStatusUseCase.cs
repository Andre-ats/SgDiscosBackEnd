using Aplicacao.UseCase.UseCasePadrao;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoMudarStatus;

public class ProdutoMudarStatusUseCase : UseCaseBase<ProdutoMudarStatusUseCaseInput, ProdutoMudarStatusUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    
    public ProdutoMudarStatusUseCase(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }
    protected override Result<ProdutoMudarStatusUseCaseOutput> ExecuteUseCase(ProdutoMudarStatusUseCaseInput input)
    {
        var produtoResult = _produtoRepositorio.GetProdutoById(input.IdProduto);

        if (produtoResult.IsFailed)
            return Result.Fail(produtoResult.Errors);

        var produtoTrocarStatus = produtoResult.Value.MudarStatus(input.StatusProduto);

        var produtoAtualizar = _produtoRepositorio.AtualizarProduto(produtoTrocarStatus);
        
        if (produtoAtualizar.IsFailed)
            return Result.Fail(produtoAtualizar.Errors);

        return Result.Ok(new ProdutoMudarStatusUseCaseOutput
        {
            Mensagem = "Produto atualizado com sucesso!"
        });
    }
}