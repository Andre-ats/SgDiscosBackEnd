using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Factory.UsuarioFactory.Validacoes;
using FluentResults;

namespace Domain.Factory.UsuarioFactory;

public class UsuarioFactory
{
    public string _Nome { get; set; }
    public string _Email { get; set; }
    public string _Senha { get; set; }
    public string _Cpf { get; set; }
    
    private UsuarioFactory(){}

    public static Result<Usuario> CriarUsuarioFactory(string nome, string email, string senha, string cpf)
    {
        var nomeValidar = NomeUsuarioValidacao.NomeValidacao(nome);
        if (nomeValidar.IsFailed) return Result.Fail(nomeValidar.Errors);
        

        Usuario usuario = Usuario.CriarUsuario(
            nome, email, senha, cpf
        );
        
        return usuario;
    }
}