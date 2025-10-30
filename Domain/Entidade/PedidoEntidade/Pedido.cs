using Domain.Entidade.PedidoEntidade.EnumsPedidoEntidade;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;

namespace Domain.Entidade.PedidoEntidade;

public class Pedido : EntidadeBase
{
    public Guid UsuarioId { get; protected set; }
    public Usuario Usuario { get; protected set; }
    public List<ProdutoPedido> ListaProdutos { get; protected set; } = new();
    public EnumPedidoStatus PedidoStatus { get; protected set; }
    
    private Pedido(){}

    public Pedido CriarPedido(Usuario usuario, List<ProdutoPedido> listaProduto)
    {
        Pedido pedido = new Pedido()
        {
            Id = Guid.NewGuid(),
            Usuario = usuario,
            UsuarioId = usuario.Id,
            ListaProdutos = listaProduto,
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now,
            PedidoStatus = PedidoStatus
        };
        
        return pedido;
    }
}