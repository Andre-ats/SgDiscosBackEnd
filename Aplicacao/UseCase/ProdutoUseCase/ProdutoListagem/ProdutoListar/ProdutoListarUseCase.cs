using Aplicacao.UseCase.UseCasePadrao;
using Aplicacao.UseCase.Utilidades.Paginacao;
using Domain.Entidade.ProdutoEntidade;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;

public class ProdutoListarUseCase : UseCaseBase<ProdutoListarUseCaseInput, ProdutoListarUseCaseOutput>
{
    private readonly IProdutoRepositorio _produtoRepositorio;
    
    public ProdutoListarUseCase(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }
    protected override Result<ProdutoListarUseCaseOutput> ExecuteUseCase(ProdutoListarUseCaseInput input)
    {
        var paginacaoResult = input.PaginacaoInput.Validar();
        if (paginacaoResult.IsFailed)
            return Result.Fail(paginacaoResult.Errors);
        
        var resultado = _produtoRepositorio.ListarProdutos(
            input.PaginacaoInput.PaginaAtual,
            input.PaginacaoInput.ItensPorPagina,
            input.NomeProduto,
            input.CodigoBarra,
            input.GeneroMusical,
            input.FormatoProduto,
            input.TipoDeAlbum,
            input.StatusProduto,
            input.ListarProdutosInativos);

        if (resultado.IsFailed)
            return Result.Fail(resultado.Errors);
        
        return Result.Ok(new ProdutoListarUseCaseOutput
        {
            PaginacaoOutput = new PaginacaoOutput<Produto>
            {
                Itens = resultado.Value.Produtos,
                TotalItens = resultado.Value.TotalItens,
                PaginaAtual = input.PaginacaoInput.PaginaAtual,
                ItensPorPagina = input.PaginacaoInput.ItensPorPagina
            }
        });
    }
}