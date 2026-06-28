using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;

public class ProdutoAdicionarArquivosUseCaseInput : UseCaseAsyncBaseInput
{
    public Guid IdProduto { get; set; }
    public List<ArquivoAdicionarInput> ArquivoAdicionarInputs { get; set; }

    public ProdutoAdicionarArquivosUseCaseInput(Guid idProduto, List<ArquivoAdicionarInput> arquivoLista)
    {
        IdProduto = idProduto;
        ArquivoAdicionarInputs = arquivoLista;
    }
}