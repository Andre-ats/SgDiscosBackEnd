using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Domain.Entidade.ProdutoEntidade;

public class ArquivosProduto
{
    public string PublicId { get; protected set; }
    public EnumTipoArquivoProduto TipoArquivoProduto { get; protected set; }
    public int Ordem { get; protected set; }
    
    private ArquivosProduto() { }

    public ArquivosProduto(string publicId, EnumTipoArquivoProduto tipoArquivoProduto, int ordem)
    {
        PublicId = publicId;
        TipoArquivoProduto = tipoArquivoProduto;
        Ordem = ordem;
    }
    
    public ArquivosProduto AtualizarOrdem(int ordem)
    {
        Ordem = ordem;
        return this;
    }
}