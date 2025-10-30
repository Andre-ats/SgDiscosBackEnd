using FluentResults;

namespace Domain.Factory.UsuarioFactory.Validacoes;

public abstract class NomeUsuarioValidacao
{
    public static Result NomeValidacao(string nome)
    {
        if (string.IsNullOrEmpty(nome) || nome.Trim().Length <= 2 || nome.Trim().Length >= 80)
        {
            return Result.Fail("Nome invalido");
        }

        return Result.Ok();
    }
}