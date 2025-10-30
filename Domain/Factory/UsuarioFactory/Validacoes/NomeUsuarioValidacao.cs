using System.Text.RegularExpressions;
using FluentResults;

namespace Domain.Factory.UsuarioFactory.Validacoes;

public abstract class NomeUsuarioValidacao
{
    private static readonly Regex Rx = new(@"^[\p{L} ]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    public static Result Validar(string nome)
    {
        if(string.IsNullOrEmpty(nome)) return Result.Fail("Nome obrigatório: não pode ser nulo ou vazio.");
        if(nome.Trim().Length <= 2)  return Result.Fail("O nome deve ter pelo menos 3 caracteres.");
        if(nome.Trim().Length >= 60) return Result.Fail("O nome deve ter no máximo 60 caracteres.");
        if(!Rx.IsMatch(nome.Trim())) return Result.Fail("Use apenas letras (incluindo acentuadas) e espaços.");
        
        return Result.Ok();
    }
}