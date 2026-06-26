using Aplicacao.UseCase.UseCasePadrao;
using Aplicacao.UseCase.Utilidades.Paginacao;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;

public class ProdutoListarUseCaseInput : UseCaseBaseInput
{
    public PaginacaoInput PaginacaoInput { get; set; } = new();
}