using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;

public class ArquivoAdicionarInput
{
    public IFormFile Arquivo { get; set; }
    public EnumTipoArquivoProduto EnumTipoArquivo { get; set; }

    public ArquivoAdicionarInput(IFormFile arquivo, EnumTipoArquivoProduto tipoArquivo)
    {
        Arquivo = arquivo;
        EnumTipoArquivo = tipoArquivo;
    }
}