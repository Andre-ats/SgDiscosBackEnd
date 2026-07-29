using Aplicacao.UseCase.UseCasePadrao;
using Domain.DTO.Produto;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;

public class ProdutoCadastrarUseCaseInput : UseCaseBaseInput
{
    public CriarProdutoDTO CriarProdutoDto { get; set; }
}