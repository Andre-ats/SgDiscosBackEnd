using Domain.Entidade.ProdutoEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.ProdutoRepositorio;

public interface IProdutoRepositorio
{
    public Result<bool> CriarProduto(Produto produto);
    public Result<(List<Produto> Produtos, int TotalItens)> ListarProdutos(int paginaAtual, int itensPorPagina);
}