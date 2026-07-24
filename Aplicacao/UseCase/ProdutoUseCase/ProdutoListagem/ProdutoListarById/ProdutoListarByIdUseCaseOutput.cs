using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListarById;

public class ProdutoListarByIdUseCaseOutput : UseCaseBaseOutput
{
    public Produto Produto { get; set; }
}