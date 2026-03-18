using Domain.Entidade.AdminEntidade;
using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoPedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.DbConfig;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }
    
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ProdutoPedido> ProdutoPedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
}