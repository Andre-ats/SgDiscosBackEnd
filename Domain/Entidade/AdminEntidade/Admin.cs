using Domain.Utilitarios;

namespace Domain.Entidade.AdminEntidade;

public class Admin : EntidadeBase
{
    public string Nome { get; protected set; }
    public string Email { get; protected set; }
    public string Senha { get; protected set; }
    
    private Admin(){}

    public Admin CriarAdmin(string nome, string email, string senha)
    {
        Admin admin = new Admin()
        {
            Id = Guid.NewGuid(),
            Nome = nome,
            Email = email,
            Senha = Hash256.CriptografiaSenha(senha),
            DataDeAtualizacao = DateTime.Now,
            DataDeCriacao = DateTime.Now
        };
        return admin;
    }
}