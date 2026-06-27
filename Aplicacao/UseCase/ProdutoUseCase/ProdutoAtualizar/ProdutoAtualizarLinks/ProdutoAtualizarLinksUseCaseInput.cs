using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks;

public class ProdutoAtualizarLinksUseCaseInput : UseCaseBaseInput
{
    public Guid IdProduto { get; set; }
    public EnumTipoArquivo TipoDoArquivo { get; set; }
    public List<string> ArquivoUrlList { get; set; }
}