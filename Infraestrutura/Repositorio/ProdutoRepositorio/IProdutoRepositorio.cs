using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.ProdutoRepositorio;

public interface IProdutoRepositorio
{
    public Result<bool> CriarProduto(Produto produto);

    public Result<(List<Produto> Produtos, int TotalItens)> ListarProdutos(
        int paginaAtual,
        int itensPorPagina,
        string? nomeProduto,
        EnumGeneroMusicalProduto? generoMusical,
        EnumFormatoProduto? formatoProduto,
        EnumTipoDeAlbum? tipoDeAlbum,
        EnumStatusProduto? statusProduto);
    public Result<bool> AtualizarProduto(Produto produto);
    public Result<Produto> GetProdutoById(Guid id);
}