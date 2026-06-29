using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoMudarStatus;

public class ProdutoMudarStatusUseCaseInput : UseCaseBaseInput
{
    public Guid IdProduto { get; set; }
    public EnumStatusProduto StatusProduto { get; set; }
}