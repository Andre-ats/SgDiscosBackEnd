using FluentResults;

namespace Domain.Factory.UsuarioFactory.Validacoes;

public class SenhaUsuarioValidacao
{
    public static Result Validar(string senha)
    {
        if(string.IsNullOrEmpty(senha)) return Result.Fail("Senha obrigatória: não pode ser nulo ou vazio.");
        if(senha.Trim().Length <= 7)  return Result.Fail("O senha deve ter pelo menos 7 caracteres.");
        if(senha.Trim().Length >= 30) return Result.Fail("O senha deve ter no máximo 30 caracteres.");
        
        return Result.Ok();
    }
}