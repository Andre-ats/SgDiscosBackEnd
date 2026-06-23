using Domain.Entidade.AdminEntidade;
using Domain.Entidade.ProdutoEntidade;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorio;

public class DataBaseContext : DbContext
{
    public DataBaseContext() { }
    
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Admin>(user =>
        {
            user.ToTable("Admin");
        });
        
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("Produtos");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.NomeProduto)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(p => p.NomeArtistaBandaProduto)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(p => p.EmpresaProduto)
                .HasMaxLength(80);

            entity.Property(p => p.OrigemProduto)
                .HasMaxLength(80);

            entity.Property(p => p.AnoLancamentoProduto);

            entity.Property(p => p.EmbalagemProduto)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(p => p.FormatoProduto)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(p => p.TipoDeAlbum)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(p => p.GenerosMusicaisProduto)
                .IsRequired()
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Enum.Parse<EnumGeneroMusicalProduto>(x))
                        .ToList()
                );

            entity.Property(p => p.QuantidadeDeCancoesProduto);

            entity.Property(p => p.QuantidadeProduto)
                .IsRequired();

            entity.Property(p => p.PrecoProduto)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.PrecoDescontoProduto)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.DataDeCriacao)
                .IsRequired();

            entity.Property(p => p.DataDeAtualizacao)
                .IsRequired();
        });
    }

    public DbSet<Admin> AdminsDB { get; set; }
    public DbSet<Produto> ProdutosDB { get; set; }
}