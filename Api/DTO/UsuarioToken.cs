namespace Api.DTO;

public class UsuarioToken
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cargo { get; set; }

    public UsuarioToken(Guid id, string nome, string email, string cargo)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Cargo = cargo;
    }
}