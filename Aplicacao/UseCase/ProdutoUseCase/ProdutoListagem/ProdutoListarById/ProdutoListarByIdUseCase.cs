using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListarById;

public class ProdutoListarByIdUseCase : UseCaseBase<ProdutoListarByIdUseCaseInput, ProdutoListarByIdUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    
    public ProdutoListarByIdUseCase(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }
    protected override Result<ProdutoListarByIdUseCaseOutput> ExecuteUseCase(ProdutoListarByIdUseCaseInput input)
    {
        var produto = _produtoRepositorio.GetProdutoById(input.ProdutoId);
        
        if(produto.IsFailed)
            return Result.Fail(produto.Errors);

        return Result.Ok(new ProdutoListarByIdUseCaseOutput
        {
            Produto = produto.Value
        });
    }
}