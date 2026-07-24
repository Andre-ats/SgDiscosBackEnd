using Domain.Entidade.AdminEntidade;
using Domain.Factory.AdminFactory.Validacoes;
using Domain.Factory.UsuarioFactory.Validacoes;
using Domain.Factory.ValidacoesGeral;
using FluentResults;

namespace Domain.Factory.AdminFactory;

public class AdminFactory
{
    public Result<Admin> CriarAdminFactory(string nome, string email, string senha)
    {
        var nomeValidar = NomeAdminValidacao.Validar(nome);
        if (nomeValidar.IsFailed) return Result.Fail(nomeValidar.Errors);

        var emailValidar = EmailUsuarioValidacao.Validar(email);
        if (emailValidar.IsFailed) return Result.Fail(emailValidar.Errors);

        var senhaValidar = SenhaUsuarioValidacao.Validar(senha);
        if (senhaValidar.IsFailed) return Result.Fail(senhaValidar.Errors);

        Admin admin = Admin.CriarAdmin(
            nome, email, senha
        );

        return Result.Ok(admin);
    }
}