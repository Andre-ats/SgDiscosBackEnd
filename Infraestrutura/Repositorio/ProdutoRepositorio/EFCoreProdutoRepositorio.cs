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
}