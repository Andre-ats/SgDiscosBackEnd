using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;

public class ArquivoExcluirInput
{
    public string PublicId { get; set; }
    public EnumTipoArquivo EnumTipoArquivo { get; set; }

    public ArquivoExcluirInput(string publicId, EnumTipoArquivo tipoArquivo)
    {
        PublicId = publicId;
        EnumTipoArquivo = tipoArquivo;
    }
}