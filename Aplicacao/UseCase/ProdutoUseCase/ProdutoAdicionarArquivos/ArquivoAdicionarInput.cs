using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;

public class ArquivoAdicionarInput
{
    public IFormFile Arquivo { get; set; }
    public EnumTipoArquivo EnumTipoArquivo { get; set; }

    public ArquivoAdicionarInput(IFormFile arquivo, EnumTipoArquivo tipoArquivo)
    {
        Arquivo = arquivo;
        EnumTipoArquivo = tipoArquivo;
    }
}