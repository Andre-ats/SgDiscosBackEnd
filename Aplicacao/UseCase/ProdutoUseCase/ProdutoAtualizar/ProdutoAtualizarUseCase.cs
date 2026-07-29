using Aplicacao.UseCase.UseCasePadrao;
using Domain.Entidade.ProdutoEntidade;
using Domain.Factory.ProdutoFactory;
using Domain.Utilitarios.Validador;
using Domain.Utilitarios.Validador.Produto;
using FluentResults;
using Infraestrutura.Repositorio.ProdutoRepositorio;

namespace Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar;

public class ProdutoAtualizarUseCase : UseCaseBase<ProdutoAtualizarUseCaseInput, ProdutoAtualizarUseCaseOutput>
{
    
    private readonly IProdutoRepositorio _produtoRepositorio;

    public ProdutoAtualizarUseCase(IProdutoRepositorio produtoRepositorio)
    {
        _produtoRepositorio = produtoRepositorio;
    }
    
    protected override Result<ProdutoAtualizarUseCaseOutput> ExecuteUseCase(ProdutoAtualizarUseCaseInput input)
    {
        var produtoResult = new ProdutoValidador().Validar(
            input.ProdutoAtualizarDto.NomeProduto,
            input.ProdutoAtualizarDto.NomeArtistaBandaProduto,
            input.ProdutoAtualizarDto.DescricaoProduto,
            input.ProdutoAtualizarDto.EmpresaProduto,
            input.ProdutoAtualizarDto.OrigemProduto,
            input.ProdutoAtualizarDto.AnoLancamentoProduto,
            input.ProdutoAtualizarDto.CodigoBarra,
            input.ProdutoAtualizarDto.EmbalagemProduto,
            input.ProdutoAtualizarDto.FormatoProduto,
            input.ProdutoAtualizarDto.TipoDeAlbum,
            input.ProdutoAtualizarDto.GenerosMusicaisProduto,
            input.ProdutoAtualizarDto.QuantidadeDeCancoesProduto,
            input.ProdutoAtualizarDto.QuantidadeProduto,
            input.ProdutoAtualizarDto.PrecoProduto,
            input.ProdutoAtualizarDto.StatusProduto,
            input.ProdutoAtualizarDto.Condicao,
            input.ProdutoAtualizarDto.QuantidadeDiscos,
            null
        );
        
        if (produtoResult.IsFailed)
            return Result.Fail(produtoResult.Errors);
        
        var produtoBancoResult =
            _produtoRepositorio.GetProdutoById(input.ProdutoAtualizarDto.Id);

        var produto = produtoBancoResult.Value.Atualizar(input.ProdutoAtualizarDto);
        
        if (produtoBancoResult.IsFailed)
            return Result.Fail(produtoBancoResult.Errors);

        var resultRespoitorio = _produtoRepositorio.AtualizarProduto(produto);
        
        if (resultRespoitorio.IsFailed)
            return Result.Fail(resultRespoitorio.Errors);
        
        return Result.Ok(new ProdutoAtualizarUseCaseOutput()
        {
            Mensagem = "Produto do id: " + produto.Id + "foi alterador com sucesso"
        });

    }
}