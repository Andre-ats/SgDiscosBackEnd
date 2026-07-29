using Aplicacao.UseCase.UseCasePadrao;
using Domain.DTO.Produto;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar;

public class ProdutoAtualizarUseCaseInput : UseCaseBaseInput
{
    public ProdutoAtualizarDTO ProdutoAtualizarDto { get; set; }
}