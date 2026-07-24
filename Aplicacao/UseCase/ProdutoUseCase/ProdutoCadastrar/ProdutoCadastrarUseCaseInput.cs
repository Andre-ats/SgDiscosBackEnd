using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;

public class ProdutoCadastrarUseCaseInput : UseCaseBaseInput
{
    public string NomeProduto { get; set; }
    public string NomeArtistaBandaProduto { get; set; }
    public string DescricaoProduto { get; set; }
    public string? EmpresaProduto { get; set; }
    public string? OrigemProduto { get; set; }
    public int? AnoLancamentoProduto { get; set; }
    public string CodigoBarra { get; set; }
    public EnumEmbalagemProduto EmbalagemProduto { get; set; }
    public EnumFormatoProduto FormatoProduto { get; set; }
    public EnumTipoDeAlbum TipoDeAlbum { get; set; }
    public EnumCondicao Condicao { get; set; }
    public List<EnumGeneroMusicalProduto> GenerosMusicaisProduto { get; set; }
    public int? QuantidadeDeCancoesProduto { get; set; }
    public int QuantidadeProduto { get; set; }
    public int QuantidadeDiscos { get; set; }
    public decimal PrecoProduto { get; set; }
    public EnumStatusProduto StatusProduto { get; set; }
}