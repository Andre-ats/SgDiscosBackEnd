using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Domain.Entidade.ProdutoEntidade;

public class Produto : EntidadeBase
{
    public string NomeProduto { get; protected set; }
    public string NomeArtistaBandaProduto { get; protected set; }
    public string? EmpresaProduto { get; protected set; }
    public string? OrigemProduto { get; protected set; }
    public int? AnoLancamentoProduto { get; protected set; }
    public EnumEmbalagemProduto EmbalagemProduto { get; protected set; }
    public EnumFormatoProduto FormatoProduto { get; protected set; }
    public EnumTipoDeAlbum TipoDeAlbum { get; protected set; }
    public List<EnumGeneroMusicalProduto> GenerosMusicaisProduto { get; protected set; }
    public List<string> ListaImagensLinks { get; protected set; } = [];
    public List<string> ListaVideosLinks { get; protected set; } = [];
    
    public int? QuantidadeDeCancoesProduto { get; protected set; }
    public int QuantidadeProduto { get; protected set; }
    public decimal PrecoProduto { get; protected set; }
    public decimal? PrecoDescontoProduto { get; protected set; }
    
    private Produto(){}

    public static Produto CriarProduto(string nome, string nomeArtistaBanda, string? empresa, 
        string? origem, int? anoLancamento, EnumEmbalagemProduto embalagem, 
        EnumFormatoProduto formato, EnumTipoDeAlbum tipoDeAlbum, List<EnumGeneroMusicalProduto> generoMusical, int? quantidadeCancoes, int quantidade, decimal preco)
    {
        Produto produto = new Produto()
        {
            Id = Guid.NewGuid(),
            NomeProduto = nome,
            NomeArtistaBandaProduto = nomeArtistaBanda,
            EmpresaProduto = empresa,
            OrigemProduto = origem,
            AnoLancamentoProduto = anoLancamento,
            EmbalagemProduto = embalagem,
            FormatoProduto = formato,
            TipoDeAlbum = tipoDeAlbum,
            GenerosMusicaisProduto = generoMusical,
            QuantidadeDeCancoesProduto = quantidadeCancoes,
            QuantidadeProduto = quantidade,
            PrecoProduto = preco,
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now,
            PrecoDescontoProduto = null
        };
        return produto;
    }

    public Produto AdicionarImagem(string urlImagem)
    {
        if (string.IsNullOrWhiteSpace(urlImagem))
            return this;
        
        ListaImagensLinks.Add(urlImagem);

        return this;
    }
    
    public Produto AdicionarVideo(string urlVideo)
    {
        if (string.IsNullOrWhiteSpace(urlVideo))
            return this;
        
        ListaVideosLinks.Add(urlVideo);

        return this;
    }

    public Produto ExcluirImagem(string urlImagem)
    {
        if (string.IsNullOrWhiteSpace(urlImagem))
            return this;

        ListaImagensLinks.Remove(urlImagem);

        return this;
    }
    
    public Produto ExcluirVideo(string urlImagem)
    {
        if (string.IsNullOrWhiteSpace(urlImagem))
            return this;

        ListaVideosLinks.Remove(urlImagem);

        return this;
    }
}