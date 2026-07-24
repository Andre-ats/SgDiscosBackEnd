using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Domain.Entidade.ProdutoEntidade;

public class Produto : EntidadeBase
{
    public string NomeProduto { get; protected set; }
    public string NomeArtistaBandaProduto { get; protected set; }
    public string DescricaoProduto { get; set; }
    public string? EmpresaProduto { get; protected set; }
    public string? OrigemProduto { get; protected set; }
    public int? AnoLancamentoProduto { get; protected set; }
    public string CodigoBarra { get; protected set; }
    public EnumEmbalagemProduto EmbalagemProduto { get; protected set; }
    public EnumFormatoProduto FormatoProduto { get; protected set; }
    public EnumTipoDeAlbum TipoDeAlbum { get; protected set; }
    public List<EnumGeneroMusicalProduto> GenerosMusicaisProduto { get; protected set; }
    public EnumCondicao Condicao { get; protected set; }
    public List<ArquivosProduto> ArquivosProdutos { get; protected set; } = new();
    public int? QuantidadeDeCancoesProduto { get; protected set; }
    public int QuantidadeProduto { get; protected set; }
    public int QuantidadeDiscos { get; protected set; }
    public decimal PrecoProduto { get; protected set; }
    public decimal? PrecoDescontoProduto { get; protected set; }
    public EnumStatusProduto StatusProduto { get; protected set; }
    
    private Produto(){}

    public static Produto CriarProduto(string nome, string nomeArtistaBanda, string descricaoProduto, string? empresa, 
        string? origem, int? anoLancamento, string codigoBarra, EnumEmbalagemProduto embalagem, 
        EnumFormatoProduto formato, EnumTipoDeAlbum tipoDeAlbum, List<EnumGeneroMusicalProduto> generoMusical, 
        int? quantidadeCancoes, int quantidade, decimal preco, EnumStatusProduto statusProduto, EnumCondicao condicao, int quantidadeDiscos)
    {
        Produto produto = new Produto()
        {
            Id = Guid.NewGuid(),
            NomeProduto = nome,
            NomeArtistaBandaProduto = nomeArtistaBanda,
            DescricaoProduto = descricaoProduto,
            EmpresaProduto = empresa,
            OrigemProduto = origem,
            AnoLancamentoProduto = anoLancamento,
            CodigoBarra = codigoBarra,
            EmbalagemProduto = embalagem,
            FormatoProduto = formato,
            TipoDeAlbum = tipoDeAlbum,
            GenerosMusicaisProduto = generoMusical,
            QuantidadeDeCancoesProduto = quantidadeCancoes,
            QuantidadeProduto = quantidade,
            PrecoProduto = preco,
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now,
            PrecoDescontoProduto = null,
            StatusProduto = statusProduto,
            Condicao = condicao,
            QuantidadeDiscos = quantidadeDiscos
        };
        return produto;
    }

    public Produto AdicionarArquivo(ArquivosProduto arquivo)
    {
        if (string.IsNullOrWhiteSpace(arquivo.PublicId))
            return this;
        
        ArquivosProdutos.Add(arquivo);

        return this;
    }
    
    public Produto ExcluirArquivo(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return this;

        var arquivo = ArquivosProdutos.FirstOrDefault(x => x.PublicId == publicId);

        if (arquivo is null)
            return this;

        ArquivosProdutos.Remove(arquivo);

        return this;
    }

    public Produto MudarStatus(EnumStatusProduto statusProduto)
    {
        if (!Enum.IsDefined(typeof(EnumStatusProduto), statusProduto))
            return this;
        
        StatusProduto = statusProduto;

        return this;
    }
    
}