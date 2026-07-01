using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Factory.ValidacoesGeral;
using FluentResults;

namespace Domain.Factory.ProdutoFactory;

public class ProdutoFactory
{
    public Result<Produto> CriarProdutoFactory(string nomeProduto, string nomeArtistaBandaProduto, string descricaoProduto, string? empresaProduto, string? origemProduto,
        int? anoLancamentoProduto, string codigoBarra, EnumEmbalagemProduto embalagemProduto, EnumFormatoProduto formatoProduto, EnumTipoDeAlbum tipoDeAlbum, 
        List<EnumGeneroMusicalProduto> generoMusicalProdutos, int? quantidadeDeCancoesProduto, int quantidadeProduto, decimal precoProduto, EnumStatusProduto statusProduto)
    {
        
        var camposString = new (string? valor, string nome, int min, int max, bool obrigatorio)[]
        {
            (nomeProduto, "Nome do Produto", 2, 80, true),
            (nomeArtistaBandaProduto, "Nome do Artista", 2, 80, true),
            (descricaoProduto, "Descricao do Produto", 10, 2000, true),
            (empresaProduto, "Empresa", 2, 80, false),
            (origemProduto, "Origem", 2, 80, false),
            (codigoBarra, "Codigo de Barra", 10, 14, true)
        };
        
        var camposInt = new (int? valor, string nome, int min, int max, bool obrigatorio)[]
        {
            (anoLancamentoProduto, "Ano de Lançamento", 1900, DateTime.UtcNow.Year + 1, false),
            (quantidadeDeCancoesProduto, "Quantia de Canções", 1, 150, false),
            (quantidadeProduto, "Quantidade de Produto", 1, 1000, true),
        };
        
        var camposDecimal = new (decimal? valor, string nome, decimal min, decimal max, bool obrigatorio)[]
        {
            (precoProduto, "Preço do Produto", 1m, 10000m, true)
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
        
        if (!Enum.IsDefined(statusProduto))
            return Result.Fail("Status inválido.");
        
        if (generoMusicalProdutos.Count == 0)
            return Result.Fail("É obrigatório informar ao menos um gênero musical.");

        if (generoMusicalProdutos.Any(g => !Enum.IsDefined(g)))
            return Result.Fail("Há gênero musical inválido.");
        
        var produto = Produto.CriarProduto(
            nomeProduto,
            nomeArtistaBandaProduto,
            descricaoProduto,
            empresaProduto,
            origemProduto,
            anoLancamentoProduto,
            codigoBarra,
            embalagemProduto,
            formatoProduto,
            tipoDeAlbum,
            generoMusicalProdutos,
            quantidadeDeCancoesProduto,
            quantidadeProduto,
            precoProduto,
            statusProduto
        );

        return Result.Ok(produto);
        
    }
}