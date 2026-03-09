using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Factory.ProdutoFactory.Validacoes;
using FluentResults;

namespace Domain.Factory.ProdutoFactory;

public class ProdutoFactory
{
    public string _NomeProduto { get; set; }
    public string _NomeArtistaBandaProduto { get; set; }
    public string? _EmpresaProduto { get; set; }
    public string? _OrigemProduto { get; set; }
    public string? _AnoLancamentoProduto { get; set; }
    public EnumEmbalagemProduto _EmbalagemProduto { get; set; }
    public EnumFormatoProduto _FormatoProduto { get; set; }
    public EnumTipoDeAlbum _TipoDeAlbum { get; set; }
    public List<EnumGeneroMusicalProduto> _GenerosMusicaisProduto { get; set; }
    public int? _QuantidadeDeCancoesProduto { get; set; }
    public int _QuantidadeProduto { get; set; }
    public decimal _PrecoProduto { get; set; }
    public decimal? _PrecoDescontoProduto { get; set; }

    public static Result<Produto> CriarProdutoFactory(string nomeProduto, string nomeArtistaBandaProduto, string? empresaProduto, string? origemProduto,
        string? anoLancamentoProduto, EnumEmbalagemProduto embalagemProduto, EnumFormatoProduto formatoProduto, EnumTipoDeAlbum tipoDeAlbum, 
        List<EnumGeneroMusicalProduto> generoMusicalProdutos, int? quantidadeDeCancoesProduto, int quantidadeProduto, decimal precoProduto, decimal? precoDescontoProduto)
    {
        var nomeProdutoValidar = ProdutoStringValidacao.Validar(nomeProduto, "Nome do Produto", 80, 2);
        if (nomeProdutoValidar.IsFailed) return Result.Fail(nomeProdutoValidar.Errors);
        return Result.Ok();
    }
}