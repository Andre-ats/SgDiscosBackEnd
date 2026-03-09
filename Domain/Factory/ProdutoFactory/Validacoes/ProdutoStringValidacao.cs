using FluentResults;

namespace Domain.Factory.ProdutoFactory.Validacoes;

public class ProdutoStringValidacao
{
    public static Result Validar(string nome, string campo, int maxLength, int minLength)
    {
        if(string.IsNullOrEmpty(nome)) return Result.Fail($"{campo} obrigatório: não pode ser nulo ou vazio.");
        if(nome.Trim().Length < minLength)  return Result.Fail($"O {campo} deve ter pelo menos {minLength} caracteres.");
        if(nome.Trim().Length > maxLength) return Result.Fail($"O nome deve ter no máximo {maxLength} caracteres.");
        
        return Result.Ok();
    }
}