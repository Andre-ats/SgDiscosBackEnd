using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using Domain.Factory.ProdutoFactory;
using Domain.Utilitarios.Validador;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;

public class ProdutoCadastrarUseCase : UseCaseBase<ProdutoCadastrarUseCaseInput, ProdutoCadastrarUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;

    public ProdutoCadastrarUseCase(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }
    
    protected override Result<ProdutoCadastrarUseCaseOutput> ExecuteUseCase(ProdutoCadastrarUseCaseInput produtoInput)
    {
        var produtoResult = new ProdutoFactory().CriarProdutoFactory(
            produtoInput.CriarProdutoDto
        );

        if (produtoResult.IsFailed)
            return Result.Fail(produtoResult.Errors);
        
        var respostaRepositorioProduto = _produtoRepositorio.CriarProduto(produtoResult.Value);
        
        if (respostaRepositorioProduto.IsFailed)
            return Result.Fail(respostaRepositorioProduto.Errors);

        return Result.Ok(new ProdutoCadastrarUseCaseOutput()
        {
            Produto = produtoResult.Value
        });

    }
}