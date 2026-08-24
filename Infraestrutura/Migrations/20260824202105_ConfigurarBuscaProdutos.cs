using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class ConfigurarBuscaProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:unaccent", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoProduto",
                table: "Produtos",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.Sql("""
                CREATE TEXT SEARCH CONFIGURATION simple_unaccent (COPY = simple);

                ALTER TEXT SEARCH CONFIGURATION simple_unaccent
                ALTER MAPPING FOR hword, hword_part, word
                WITH unaccent, simple;
            """);

            migrationBuilder.Sql("""
                CREATE INDEX "IX_Produtos_FullText"
                ON "Produtos"
                USING GIN (
                    to_tsvector(
                        'simple_unaccent',
                        coalesce("NomeProduto", '') || ' ' ||
                        coalesce("NomeArtistaBandaProduto", '') || ' ' ||
                        coalesce("DescricaoProduto", '')
                    )
                );
            """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DROP INDEX IF EXISTS "IX_Produtos_FullText";
            """);

            migrationBuilder.Sql("""
                DROP TEXT SEARCH CONFIGURATION IF EXISTS simple_unaccent;
            """);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoProduto",
                table: "Produtos",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:unaccent", ",,");
        }
    }
}