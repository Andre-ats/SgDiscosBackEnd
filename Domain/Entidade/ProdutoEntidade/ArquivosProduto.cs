using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Domain.Entidade.ProdutoEntidade;

public class ArquivosProduto
{
    public string PublicId { get; protected set; }
    public EnumTipoArquivoProduto TipoArquivoProduto { get; protected set; }
    
    private ArquivosProduto() { }

    public ArquivosProduto(string publicId, EnumTipoArquivoProduto tipoArquivoProduto)
    {
        PublicId = publicId;
        TipoArquivoProduto = tipoArquivoProduto;
    }
}