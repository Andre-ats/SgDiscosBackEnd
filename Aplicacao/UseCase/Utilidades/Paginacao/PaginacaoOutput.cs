namespace Aplicacao.UseCase.Utilidades.Paginacao;

public class PaginacaoOutput<T>
{
    public List<T> Itens { get; set; } = [];
    public int TotalItens { get; set; }
    public int PaginaAtual { get; set; }
    public int ItensPorPagina { get; set; }
    public int TotalPaginas =>
        (int)Math.Ceiling((double)TotalItens / ItensPorPagina);
}