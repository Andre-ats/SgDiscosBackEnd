using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using FluentResults;

namespace Domain.Factory.PedidoFactory;

public class PedidoFactory
{
    public Result<Pedido> CriarPedidoFactory(Usuario usuario, List<ProdutoPedido> listaProduto)
    {
        if (listaProduto.Count == 0)
            return Result.Fail("O pedido deve ter ao menos um produto.");

        var pedido = Pedido.CriarPedido(
            usuario,
            listaProduto
        );
        
        return Result.Ok(pedido);
    }
}