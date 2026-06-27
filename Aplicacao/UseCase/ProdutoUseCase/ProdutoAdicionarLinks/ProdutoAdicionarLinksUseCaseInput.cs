using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarLinks;

public class ProdutoAdicionarLinksUseCaseInput : UseCaseAsyncBaseInput
{
    public Guid IdProduto { get; set; }
    public EnumTipoArquivo TipoDoArquivo { get; set; }
    public List<IFormFile> ArquivoLista { get; set; }

    public ProdutoAdicionarLinksUseCaseInput(Guid idProduto, EnumTipoArquivo tipoArquivo, List<IFormFile> arquivoLista)
    {
        IdProduto = idProduto;
        TipoDoArquivo = tipoArquivo;
        ArquivoLista = arquivoLista;
    }
}