using Aplicacao.UseCase.UseCasePadrao;
using Aplicacao.UseCase.Utilidades.Paginacao;
using Domain.Entidade.ProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;

public class ProdutoListarUseCaseOutput : UseCaseBaseOutput
{
    public PaginacaoOutput<Produto> PaginacaoOutput { get; set; } = new();
}