using Aplicacao.UseCase.UseCasePadrao;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListarById;

public class ProdutoListarByIdUseCaseInput : UseCaseBaseInput
{
    public Guid ProdutoId { get; set; }
}