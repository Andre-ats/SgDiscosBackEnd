using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using FluentResults;
using Microsoft.EntityFrameworkCore;

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

    public Result<(List<Produto> Produtos, int TotalItens)> ListarProdutos(
    int paginaAtual,
    int itensPorPagina,
    string? nomeProduto,
    string? codigoBarra,
    EnumGeneroMusicalProduto? generoMusical,
    EnumFormatoProduto? formatoProduto,
    EnumTipoDeAlbum? tipoDeAlbum,
    EnumStatusProduto? statusProduto,
    bool listarInativos = true)
    {
        try
        {
            var query = _dataBaseContext.ProdutosDB
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomeProduto))
            {
                var nome = nomeProduto.ToLower();

                query = query.Where(x =>
                    x.NomeProduto.ToLower().Contains(nome) ||
                    x.NomeArtistaBandaProduto.ToLower().Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(codigoBarra))
            {
                var codigo = codigoBarra.ToLower();

                query = query.Where(x =>
                    x.CodigoBarra.ToLower().Contains(codigo));
            }

            if (formatoProduto.HasValue)
                query = query.Where(x => x.FormatoProduto == formatoProduto.Value);

            if (tipoDeAlbum.HasValue)
                query = query.Where(x => x.TipoDeAlbum == tipoDeAlbum.Value);
            
            if (statusProduto.HasValue)
            {
                query = query.Where(x => x.StatusProduto == statusProduto.Value);
            }
            else if (!listarInativos)
            {
                query = query.Where(x => x.StatusProduto != EnumStatusProduto.Inativo);
            }

            var lista = query.ToList();

            foreach (var produto in lista)
            {
                produto.ArquivosProdutos.Sort((a, b) => a.Ordem.CompareTo(b.Ordem));
            }

            if (generoMusical.HasValue)
            {
                lista = lista
                    .Where(x => x.GenerosMusicaisProduto.Contains(generoMusical.Value))
                    .ToList();
            }

            var totalItens = lista.Count;

            var produtos = lista
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
        try
        {
            var produto = _dataBaseContext.ProdutosDB
                .FirstOrDefault(x => x.Id == id);

            if (produto is null)
                return Result.Fail("Produto não encontrado.");

            produto.ArquivosProdutos.Sort((a, b) => a.Ordem.CompareTo(b.Ordem));

            return Result.Ok(produto);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Erro ao buscar o produto: {ex.Message}");
        }
    }
}