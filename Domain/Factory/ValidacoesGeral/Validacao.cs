using FluentResults;

namespace Domain.Factory.ValidacoesGeral;

public class Validacao
{
    public static Result String(string nome, string campo, int min, int max)
    {
        if(string.IsNullOrEmpty(nome)) return Result.Fail($"{campo} obrigatório: não pode ser nulo ou vazio.");
        if(nome.Trim().Length < min)  return Result.Fail($"O {campo} deve ter pelo menos {min} caracteres.");
        if(nome.Trim().Length > max) return Result.Fail($"O nome deve ter no máximo {max} caracteres.");
        
        return Result.Ok();
    }
    
    public static Result Int(int valor, string campo, int min, int max)
    {
        if (valor < min)
            return Result.Fail($"{campo} deve ser maior ou igual a {min}.");

        if (valor > max)
            return Result.Fail($"{campo} deve ser menor ou igual a {max}.");

        return Result.Ok();
    }

    public static Result Decimal(decimal valor, string campo, decimal min, decimal max)
    {
        if (valor < min)
            return Result.Fail($"{campo} deve ser maior ou igual a {min}.");

        if (valor > max)
            return Result.Fail($"{campo} deve ser menor ou igual a {max}.");

        return Result.Ok();
    }
}