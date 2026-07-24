using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Migration001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeProduto = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    NomeArtistaBandaProduto = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    DescricaoProduto = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    EmpresaProduto = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    OrigemProduto = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    AnoLancamentoProduto = table.Column<int>(type: "integer", nullable: true),
                    CodigoBarra = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    EmbalagemProduto = table.Column<string>(type: "text", nullable: false),
                    FormatoProduto = table.Column<string>(type: "text", nullable: false),
                    TipoDeAlbum = table.Column<string>(type: "text", nullable: false),
                    GenerosMusicaisProduto = table.Column<string>(type: "text", nullable: false),
                    QuantidadeDeCancoesProduto = table.Column<int>(type: "integer", nullable: true),
                    QuantidadeProduto = table.Column<int>(type: "integer", nullable: false),
                    PrecoProduto = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PrecoDescontoProduto = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StatusProduto = table.Column<string>(type: "text", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoArquivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false),
                    TipoArquivoProduto = table.Column<string>(type: "text", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoArquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoArquivos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoArquivos_ProdutoId",
                table: "ProdutoArquivos",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ProdutoArquivos");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
