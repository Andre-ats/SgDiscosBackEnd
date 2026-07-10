using Aplicacao.UseCase.UseCasePadrao;
using Aplicacao.UseCase.Utilidades.Paginacao;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;

public class ProdutoListarUseCaseInput : UseCaseBaseInput
{
    public PaginacaoInput PaginacaoInput { get; set; } = new();
    public string? NomeProduto { get; set; }
    public string? CodigoBarra { get; set; }
    public EnumGeneroMusicalProduto? GeneroMusical { get; set; }
    public EnumFormatoProduto? FormatoProduto { get; set; }
    public EnumTipoDeAlbum? TipoDeAlbum { get; set; }
    public EnumStatusProduto? StatusProduto { get; set; }
}