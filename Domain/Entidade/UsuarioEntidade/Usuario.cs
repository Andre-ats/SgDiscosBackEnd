using Domain.Utilitarios;

namespace Domain.Entidade.UsuarioEntidade;

public class Usuario : EntidadeBase
{
    public string Nome { get; protected set; }
    public string Email { get; protected set; }
    public string Senha { get; protected set; }
    public string Cpf { get; protected set; }
    public DateTime DataDeCriacao { get; protected set; }
    
    private Usuario(){}

    public static Usuario CriarUsuario(string nome, string email, string senha, string cpf, DateTime dataDeCriacao)
    {
        Usuario usuario = new Usuario()
        {
            Id = Guid.NewGuid(),
            Nome = nome,
            Email = email,
            Senha = Hash256.CriptografiaSenha(senha),
            Cpf = cpf,
            DataDeCriacao = dataDeCriacao
        };

        return usuario;
    }
}