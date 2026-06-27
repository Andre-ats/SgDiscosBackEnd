using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Migration01 : Migration
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
                    EmpresaProduto = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    OrigemProduto = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    AnoLancamentoProduto = table.Column<int>(type: "integer", nullable: true),
                    EmbalagemProduto = table.Column<string>(type: "text", nullable: false),
                    FormatoProduto = table.Column<string>(type: "text", nullable: false),
                    TipoDeAlbum = table.Column<string>(type: "text", nullable: false),
                    GenerosMusicaisProduto = table.Column<string>(type: "text", nullable: false),
                    ListaImagensLinks = table.Column<List<string>>(type: "text[]", nullable: false),
                    ListaVideosLinks = table.Column<List<string>>(type: "text[]", nullable: false),
                    QuantidadeDeCancoesProduto = table.Column<int>(type: "integer", nullable: true),
                    QuantidadeProduto = table.Column<int>(type: "integer", nullable: false),
                    PrecoProduto = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PrecoDescontoProduto = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    DataDeCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
