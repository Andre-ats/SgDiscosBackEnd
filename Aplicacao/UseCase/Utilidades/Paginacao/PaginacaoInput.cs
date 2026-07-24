using FluentResults;

namespace Aplicacao.UseCase.Utilidades.Paginacao;

public class PaginacaoInput
{
    public int PaginaAtual { get; set; } = 1;
    public int ItensPorPagina { get; set; } = 10;
    
    public Result Validar()
    {
        if (PaginaAtual < 1)
            return Result.Fail("A página atual deve ser maior que zero.");

        if (ItensPorPagina < 1)
            return Result.Fail("A quantidade de itens por página deve ser maior que zero.");

        if (ItensPorPagina > 100)
            return Result.Fail("O máximo permitido é 100 itens por página.");

        return Result.Ok();
    }
}