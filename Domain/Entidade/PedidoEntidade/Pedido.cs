using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.UsuarioEntidade;

namespace Domain.Entidade.PedidoEntidade;

public class Pedido : EntidadeBase
{
    public Guid UsuarioId { get; protected set; }
    public Usuario Usuario { get; protected set; }
    public List<Produto> ListaProdutos { get; protected set; } = new();
    
    private Pedido(){}

    public Pedido CriarPedido(Usuario usuario, List<Produto> listaProduto)
    {
        Pedido pedido = new Pedido()
        {
            Id = Guid.NewGuid(),
            Usuario = usuario,
            UsuarioId = usuario.Id,
            ListaProdutos = listaProduto,
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now
        };
        
        return pedido;
    }
}