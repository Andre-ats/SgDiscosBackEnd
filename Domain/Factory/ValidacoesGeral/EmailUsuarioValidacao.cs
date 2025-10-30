using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentResults;

namespace Domain.Factory.ValidacoesGeral;

public static class EmailUsuarioValidacao
{
    private static readonly Regex RxLocal =
        new(@"^[A-Za-z0-9!#$%&'*+/=?^_`{|}~.-]+$", RegexOptions.Compiled);

    private static readonly Regex RxDomainAscii =
        new(@"^[A-Za-z0-9.-]+$", RegexOptions.Compiled);

    public static Result<string> Validar(string? email)
    {
        var e = (email ?? string.Empty).Trim().Normalize(NormalizationForm.FormC);

        if (e.Length == 0)            return Result.Fail("E-mail obrigatório.");
        if (e.Length > 254)           return Result.Fail("E-mail muito longo (máximo 254).");

        var at = e.LastIndexOf('@');
        if (at <= 0 || at == e.Length - 1)
            return Result.Fail("E-mail inválido: faltando parte local ou domínio.");

        var local = e[..at];
        var domain = e[(at + 1)..];

        if (local.Length > 64)                     return Result.Fail("Parte local do e-mail muito longa (máximo 64).");
        if (local.StartsWith('.') || local.EndsWith('.') || local.Contains(".."))
                                                   return Result.Fail("Parte local inválida: pontos duplicados ou nos extremos.");
        if (!RxLocal.IsMatch(local))               return Result.Fail("Caracteres inválidos na parte local do e-mail.");

        string asciiDomain;
        try
        {
            var idn = new IdnMapping();
            asciiDomain = string.Join(".",
                domain.Split('.', StringSplitOptions.RemoveEmptyEntries)
                      .Select(label => idn.GetAscii(label)));
        }
        catch
        {
            return Result.Fail("Domínio do e-mail inválido.");
        }

        if (!RxDomainAscii.IsMatch(asciiDomain))   return Result.Fail("Domínio do e-mail inválido.");
        var labels = asciiDomain.Split('.');
        if (labels.Length < 2)                     return Result.Fail("Domínio do e-mail inválido.");
        if (labels.Any(l => l.Length is 0 or > 63))return Result.Fail("Rótulo do domínio com tamanho inválido.");
        if (labels.Any(l => l.StartsWith('-') || l.EndsWith('-')))
                                                   return Result.Fail("Rótulo do domínio não pode iniciar/terminar com hífen.");
        if (labels.Last().Length < 2)              return Result.Fail("TLD do e-mail inválido.");

        var normalizado = $"{local}@{asciiDomain}".ToLowerInvariant();
        return Result.Ok(normalizado);
    }
}
