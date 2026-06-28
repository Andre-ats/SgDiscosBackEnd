using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;

public class ArquivoExcluirInput
{
    public string PublicId { get; set; }
    public EnumTipoArquivoProduto EnumTipoArquivo { get; set; }

    public ArquivoExcluirInput(string publicId, EnumTipoArquivoProduto tipoArquivo)
    {
        PublicId = publicId;
        EnumTipoArquivo = tipoArquivo;
    }
}