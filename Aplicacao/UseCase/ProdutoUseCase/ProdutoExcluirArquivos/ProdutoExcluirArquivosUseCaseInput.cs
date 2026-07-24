using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;

public class ProdutoExcluirArquivosUseCaseInput : UseCaseAsyncBaseInput
{
    public Guid IdProduto { get; set; }
    public List<ArquivoExcluirInput> ArquivoLista { get; set; }

    public ProdutoExcluirArquivosUseCaseInput(Guid idProduto, List<ArquivoExcluirInput> arquivoExcluirInputs)
    {
        IdProduto = idProduto;
        ArquivoLista = arquivoExcluirInputs;
    }
}