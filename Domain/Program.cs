using Domain.Entidade.UsuarioEntidade;
using Domain.Factory.UsuarioFactory;

var r = UsuarioFactory.CriarUsuarioFactory("Andre", "kawopen419@filipx.com", "Teste@123", "47138347855");


if (r.IsSuccess)
    Console.WriteLine($"{r.Value.Nome} | {r.Value.Email} | {r.Value.Cpf}");
else
    Console.WriteLine(string.Join(" | ", r.Errors.Select(e => e.Message)));