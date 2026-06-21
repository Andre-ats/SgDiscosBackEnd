using Aplicacao.UseCase.UseCaseBase;
using Domain.Entidade.ProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;

public class ProdutoCadastrarUseCaseInput : UseCaseBaseInput
{
    public Produto Produto;
}