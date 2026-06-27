using System.Data.Entity;
using Domain.Entidade.ProdutoEntidade;
using FluentResults;

namespace Infraestrutura.Repositorio.ProdutoRepositorio;

public class EFCoreProdutoRepositorio : IProdutoRepositorio
{
    
    private readonly DataBaseContext _dataBaseContext = null!;

    public EFCoreProdutoRepositorio(DataBaseContext context)
    {
        _dataBaseContext = context;
    }
    
    public Result<bool> CriarProduto(Produto produto)
    {
        var existe = _dataBaseContext.ProdutosDB.Any(x =>
            x.NomeProduto == produto.NomeProduto &&
            x.NomeArtistaBandaProduto == produto.NomeArtistaBandaProduto
        );

        if (existe)
            return Result.Fail("Produto já cadastrado.");
        
        _dataBaseContext.ProdutosDB.Add(produto);
        var saved = _dataBaseContext.SaveChanges() > 0;

        return saved
            ? Result.Ok(true)
            : Result.Fail("Erro ao salvar o item.");
    }

    public Result<(List<Produto> Produtos, int TotalItens)> ListarProdutos(int paginaAtual, int itensPorPagina)
    {
        try
        {
            var query = _dataBaseContext.ProdutosDB.AsNoTracking();

            var totalItens = query.Count();

            var produtos = query
                .Skip((paginaAtual - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();

            return Result.Ok((produtos, totalItens));
        }
        catch (Exception ex)
        {
            return Result.Fail($"Erro ao listar produtos: {ex.Message}");
        }
    }

    public Result<bool> AtualizarProduto(Produto produto)
    {
        try
        {
            _dataBaseContext.ProdutosDB.Update(produto);

            var atualizado = _dataBaseContext.SaveChanges() > 0;

            return atualizado
                ? Result.Ok(true)
                : Result.Fail("Nenhum registro foi atualizado.");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Erro ao atualizar o produto: {ex.Message}");
        }
    }

    public Result<Produto> GetProdutoById(Guid id)
    {
        throw new NotImplementedException();
    }
}