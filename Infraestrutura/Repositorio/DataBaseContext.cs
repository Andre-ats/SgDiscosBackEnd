using Domain.Entidade.AdminEntidade;
using Domain.Entidade.ProdutoEntidade;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public DataBaseContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql("Include Error Detail=True");

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    
    }

    public DbSet<Admin> AdminsDB { get; set; }
    public DbSet<Produto> ProdutosDB { get; set; }
}