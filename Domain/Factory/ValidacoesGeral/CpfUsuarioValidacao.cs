using System.Text.RegularExpressions;
using FluentResults;

namespace Domain.Factory.ValidacoesGeral;

public abstract class CpfUsuarioValidacao
{
    public static Result<string> Validar(string? cpf)
    {
        var d = Regex.Replace(cpf ?? string.Empty, @"\D", "");

        if (d.Length == 0)  return Result.Fail("CPF obrigatório.");
        if (d.Length != 11) return Result.Fail("CPF deve conter 11 dígitos.");
        if (new string(d[0], 11) == d) return Result.Fail("CPF inválido.");
        if (!VerificarDigitos(d))      return Result.Fail("CPF inválido.");

        return Result.Ok(d);
    }

    private static bool VerificarDigitos(string d)
    {
        int soma = 0;
        for (int i = 0; i < 9; i++) soma += (d[i] - '0') * (10 - i);
        int dv1 = soma % 11; dv1 = dv1 < 2 ? 0 : 11 - dv1;
        if (dv1 != d[9] - '0') return false;

        soma = 0;
        for (int i = 0; i < 10; i++) soma += (d[i] - '0') * (11 - i);
        int dv2 = soma % 11; dv2 = dv2 < 2 ? 0 : 11 - dv2;
        return dv2 == d[10] - '0';
    }
    
}