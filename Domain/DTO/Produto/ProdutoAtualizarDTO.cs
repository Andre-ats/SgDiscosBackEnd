using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Domain.DTO.Produto;

public class ProdutoAtualizarDTO
{
    public Guid Id { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public string NomeArtistaBandaProduto { get; set; } = string.Empty;
    public string DescricaoProduto { get; set; } = string.Empty;
    public string? EmpresaProduto { get; set; }
    public string? OrigemProduto { get; set; }
    public int? AnoLancamentoProduto { get; set; }
    public string CodigoBarra { get; set; } = string.Empty;

    public EnumEmbalagemProduto EmbalagemProduto { get; set; }
    public EnumFormatoProduto FormatoProduto { get; set; }
    public EnumTipoDeAlbum TipoDeAlbum { get; set; }
    public EnumCondicao Condicao { get; set; }

    public List<EnumGeneroMusicalProduto> GenerosMusicaisProduto { get; set; } = [];

    public int? QuantidadeDeCancoesProduto { get; set; }
    public int QuantidadeProduto { get; set; }
    public int QuantidadeDiscos { get; set; }
    public decimal PrecoProduto { get; set; }
    public EnumStatusProduto StatusProduto { get; set; }
}