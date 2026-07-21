using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Factory.ProdutoFactory;
using Domain.Factory.UsuarioFactory;

//var r = UsuarioFactory.CriarUsuarioFactory("And", "kawopen419@filipx.com", "Teste@123", "47138347855");
ProdutoFactory produtoFactory = new ProdutoFactory();
var r = produtoFactory.CriarProdutoFactory(
    "Astro World",
    "Travis Scott",
    "aaaaaaaaaaaaaaaaaaaaaaaaa",
    null,
    "EUA",
    2010,
    "123123123123",
    EnumEmbalagemProduto.Lacrado,
    EnumFormatoProduto.Vinil,
    EnumTipoDeAlbum.Album,
    new List<EnumGeneroMusicalProduto>()
    {
        EnumGeneroMusicalProduto.Ambient
    },
    null,
    10,
    100m,
    EnumStatusProduto.Ativo,
    EnumCondicao.Novo,
    10
);


if (r.IsSuccess)
    Console.WriteLine($"Produto cadastrado: {r.Value.NomeProduto} - {r.Value.NomeArtistaBandaProduto}");
else
    Console.WriteLine(string.Join(" | ", r.Errors.Select(e => e.Message)));