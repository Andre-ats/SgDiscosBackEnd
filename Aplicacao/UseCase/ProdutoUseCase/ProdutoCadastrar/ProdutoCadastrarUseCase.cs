using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using Domain.Factory.ProdutoFactory;
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
    
    protected override Result<ProdutoCadastrarUseCaseOutput> ExecuteUseCase(ProdutoCadastrarUseCaseInput input)
    {
        var produtoInput = input.Produto;
        
        var produtoResult = new ProdutoFactory().CriarProdutoFactory(
            produtoInput.NomeProduto,
            produtoInput.NomeArtistaBandaProduto,
            produtoInput.EmpresaProduto,
            produtoInput.OrigemProduto,
            produtoInput.AnoLancamentoProduto,
            produtoInput.EmbalagemProduto,
            produtoInput.FormatoProduto,
            produtoInput.TipoDeAlbum,
            produtoInput.GenerosMusicaisProduto,
            produtoInput.QuantidadeDeCancoesProduto,
            produtoInput.QuantidadeProduto,
            produtoInput.PrecoProduto
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