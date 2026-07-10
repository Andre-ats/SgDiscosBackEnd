using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;

public class ArquivoAdicionarInput
{
    public IFormFile Arquivo { get; set; }
    public EnumTipoArquivoProduto EnumTipoArquivo { get; set; }
    public int Ordem { get; set; }

    public ArquivoAdicionarInput(IFormFile arquivo, EnumTipoArquivoProduto tipoArquivo, int ordem)
    {
        Arquivo = arquivo;
        EnumTipoArquivo = tipoArquivo;
        Ordem = ordem;
    }
}