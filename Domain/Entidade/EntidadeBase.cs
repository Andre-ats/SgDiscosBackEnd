namespace Domain.Entidade;

public class EntidadeBase
{
    public Guid Id { get; protected set; }
    public DateTime DataDeCriacao { get; protected set; }
    public DateTime? DataDeAtualizacao { get; protected set; }
}