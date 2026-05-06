using Domain.Entidade.ProdutoEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.ProdutoRepositorio;

public interface IProdutoRepositorio
{
    public Result<bool> CriarProduto(Produto produto);
}