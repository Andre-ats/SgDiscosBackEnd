using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Factory.ValidacoesGeral;
using FluentResults;

namespace Domain.Factory.ProdutoFactory;

public class ProdutoFactory
{
    public static Result<Produto> CriarProdutoFactory(string nomeProduto, string nomeArtistaBandaProduto, string? empresaProduto, string? origemProduto,
        int? anoLancamentoProduto, EnumEmbalagemProduto embalagemProduto, EnumFormatoProduto formatoProduto, EnumTipoDeAlbum tipoDeAlbum, 
        List<EnumGeneroMusicalProduto> generoMusicalProdutos, int? quantidadeDeCancoesProduto, int quantidadeProduto, decimal precoProduto)
    {
        
        var camposString = new (string? valor, string nome, int max, int min, bool obrigatorio)[]
        {
            (nomeProduto, "Nome do Produto", 80, 2, true),
            (nomeArtistaBandaProduto, "Nome do Artista", 80, 2, true),
            (empresaProduto, "Empresa", 80, 2, false),
            (origemProduto, "Origem", 80, 2, false)
        };
        
        var camposInt = new (int? valor, string nome, int max, int min, bool obrigatorio)[]
        {
            (anoLancamentoProduto, "Ano de Lançamento", DateTime.UtcNow.Year + 1, 1900, false),
            (quantidadeDeCancoesProduto, "Quantia de Canções", 150, 1, false),
            (quantidadeProduto, "Quantidade de Produto", 1000, 1, true),
        };
        
        var camposDecimal = new (decimal? valor, string nome, decimal max, decimal min, bool obrigatorio)[]
        {
            (precoProduto, "Preço do Produto", 10000m, 1m, true)
        };
        
        foreach (var campo in camposString)
        {
            if (!campo.obrigatorio && string.IsNullOrWhiteSpace(campo.valor))
                continue;

            var resultado = Validacao.String(campo.valor!, campo.nome, campo.min, campo.max);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);
        }
        
        foreach (var campo in camposInt)
        {
            if (!campo.obrigatorio && campo.valor is null)
                continue;

            if (campo.obrigatorio && campo.valor is null)
                return Result.Fail($"{campo.nome} é obrigatório.");

            var resultado = Validacao.Int(campo.valor!.Value, campo.nome, campo.min, campo.max);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);
        }
        
        foreach (var campo in camposDecimal)
        {
            if (!campo.obrigatorio && campo.valor is null)
                continue;

            if (campo.obrigatorio && campo.valor is null)
                return Result.Fail($"{campo.nome} é obrigatório.");

            var resultado = Validacao.Decimal(campo.valor!.Value, campo.nome, campo.min, campo.max);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);
        }
        
        if (!Enum.IsDefined(embalagemProduto))
            return Result.Fail("Embalagem inválida.");

        if (!Enum.IsDefined(formatoProduto))
            return Result.Fail("Formato inválido.");

        if (!Enum.IsDefined(tipoDeAlbum))
            return Result.Fail("Tipo de álbum inválido.");
        
        if (generoMusicalProdutos.Count == 0)
            return Result.Fail("É obrigatório informar ao menos um gênero musical.");

        if (generoMusicalProdutos.Any(g => !Enum.IsDefined(g)))
            return Result.Fail("Há gênero musical inválido.");
        
        var produto = Produto.CriarProduto(
            nomeProduto,
            nomeArtistaBandaProduto,
            empresaProduto,
            origemProduto,
            anoLancamentoProduto,
            embalagemProduto,
            formatoProduto,
            tipoDeAlbum,
            generoMusicalProdutos,
            quantidadeDeCancoesProduto,
            quantidadeProduto,
            precoProduto
        );

        return Result.Ok(produto);
        
    }
}