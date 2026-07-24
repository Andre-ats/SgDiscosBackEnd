using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Factory.ValidacoesGeral;
using FluentResults;

namespace Domain.Factory.ProdutoPedidoFactory;

public class ProdutoPedidoFactory
{
    public Result<ProdutoPedido> CriarProdutoPedidoFactory(Pedido pedido, Produto produto, int quantiaProduto)
    {
        var quantiaProdutoValidacao = Validacao.Int(quantiaProduto, "Quantia de produto", 1, 100);
        if (quantiaProdutoValidacao.IsFailed) 
            return Result.Fail(quantiaProdutoValidacao.Errors);

        var produtoPedido = ProdutoPedido.CriarProdutoPedido(
            pedido,
            produto,
            quantiaProduto
        );

        return Result.Ok(produtoPedido);

    }
}