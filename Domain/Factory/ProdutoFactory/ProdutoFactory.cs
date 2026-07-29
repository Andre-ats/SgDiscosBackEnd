using Domain.DTO.Produto;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Factory.ValidacoesGeral;
using Domain.Utilitarios.Validador;
using Domain.Utilitarios.Validador.Produto;
using FluentResults;

namespace Domain.Factory.ProdutoFactory;

public class ProdutoFactory
{
    public Result<Produto> CriarProdutoFactory(CriarProdutoDTO criarProdutoDto)
    {

        var produtoValidador = new ProdutoValidador().Validar(
            criarProdutoDto.NomeProduto,
            criarProdutoDto.NomeArtistaBandaProduto,
            criarProdutoDto.DescricaoProduto,
            criarProdutoDto.EmpresaProduto,
            criarProdutoDto.OrigemProduto,
            criarProdutoDto.AnoLancamentoProduto,
            criarProdutoDto.CodigoBarra,
            criarProdutoDto.EmbalagemProduto,
            criarProdutoDto.FormatoProduto,
            criarProdutoDto.TipoDeAlbum,
            criarProdutoDto.GenerosMusicaisProduto,
            criarProdutoDto.QuantidadeDeCancoesProduto,
            criarProdutoDto.QuantidadeProduto,
            criarProdutoDto.PrecoProduto,
            criarProdutoDto.StatusProduto,
            criarProdutoDto.Condicao,
            criarProdutoDto.QuantidadeDiscos,
            null
            );
        
        if (produtoValidador.IsFailed)
            return Result.Fail(produtoValidador.Errors);
        
        var produto = Produto.CriarProduto(
            criarProdutoDto.NomeProduto,
            criarProdutoDto.NomeArtistaBandaProduto,
            criarProdutoDto.DescricaoProduto,
            criarProdutoDto.EmpresaProduto,
            criarProdutoDto.OrigemProduto,
            criarProdutoDto.AnoLancamentoProduto,
            criarProdutoDto.CodigoBarra,
            criarProdutoDto.EmbalagemProduto,
            criarProdutoDto.FormatoProduto,
            criarProdutoDto.TipoDeAlbum,
            criarProdutoDto.GenerosMusicaisProduto,
            criarProdutoDto.QuantidadeDeCancoesProduto,
            criarProdutoDto.QuantidadeProduto,
            criarProdutoDto.PrecoProduto,
            criarProdutoDto.StatusProduto,
            criarProdutoDto.Condicao,
            criarProdutoDto.QuantidadeDiscos
        );

        return Result.Ok(produto);
        
    }
}