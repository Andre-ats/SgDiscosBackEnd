using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Factory.ProdutoFactory;
using Domain.Factory.UsuarioFactory;

//var r = UsuarioFactory.CriarUsuarioFactory("And", "kawopen419@filipx.com", "Teste@123", "47138347855");
var r = ProdutoFactory.CriarProdutoFactory(
    "p00",
    "Artista Teste",
    null,
    null,
    null,
    EnumEmbalagemProduto.Vazio,
    EnumFormatoProduto.Vazio,
    EnumTipoDeAlbum.Vazio,
    new List<EnumGeneroMusicalProduto>(),
    null,
    1,
    10m,
    null
);


if (r.IsSuccess)
    Console.WriteLine($"Produto cadastrado: {r.Value.NomeProduto} - {r.Value.NomeArtistaBandaProduto}");
else
    Console.WriteLine(string.Join(" | ", r.Errors.Select(e => e.Message)));