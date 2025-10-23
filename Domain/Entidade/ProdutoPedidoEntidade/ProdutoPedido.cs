using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoEntidade;

namespace Domain.Entidade.ProdutoPedidoEntidade;

public class ProdutoPedido : EntidadeBase
{
    public Guid IdPedido { get; protected set; }
    public Pedido Pedido { get; protected set; }
    public Guid IdProduto { get; protected set; }
    public Produto Produto { get; protected set; }
    public int QuantidadeProdutoUnico { get; protected set; }
    
    private ProdutoPedido(){}
    

    public ProdutoPedido CriarProdutoPedido(Pedido pedido, Produto produto, int quantidadeProdutoUnico)
    {
        ProdutoPedido produtoPedido = new ProdutoPedido()
        {
            Id = Guid.NewGuid(),
            IdPedido = pedido.Id,
            IdProduto = produto.Id,
            Pedido = pedido,
            Produto = produto,
            QuantidadeProdutoUnico = quantidadeProdutoUnico,
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now
        };

        return produtoPedido;
    }
}