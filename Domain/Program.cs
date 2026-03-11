using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Factory.ProdutoFactory;
using Domain.Factory.UsuarioFactory;

//var r = UsuarioFactory.CriarUsuarioFactory("And", "kawopen419@filipx.com", "Teste@123", "47138347855");
var r = ProdutoFactory.CriarProdutoFactory(
    "Astro World",
    "Travis Scott",
    null,
    "EUA",
    2010,
    EnumEmbalagemProduto.Vazio,
    EnumFormatoProduto.Vazio,
    EnumTipoDeAlbum.Vazio,
    new List<EnumGeneroMusicalProduto>()
    {
        EnumGeneroMusicalProduto.Teste
    },
    null,
    10,
    100m
);


if (r.IsSuccess)
    Console.WriteLine($"Produto cadastrado: {r.Value.NomeProduto} - {r.Value.NomeArtistaBandaProduto}");
else
    Console.WriteLine(string.Join(" | ", r.Errors.Select(e => e.Message)));