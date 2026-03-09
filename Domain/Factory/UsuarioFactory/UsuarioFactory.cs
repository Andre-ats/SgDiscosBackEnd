using Domain.Entidade.PedidoEntidade;
using Domain.Entidade.UsuarioEntidade;
using Domain.Factory.UsuarioFactory.Validacoes;
using Domain.Factory.ValidacoesGeral;
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
        var nomeValidar = NomeUsuarioValidacao.Validar(nome);
        if (nomeValidar.IsFailed) return Result.Fail(nomeValidar.Errors);

        var cpfValidar = CpfUsuarioValidacao.Validar(cpf);
        if (cpfValidar.IsFailed) return Result.Fail(cpfValidar.Errors);
        
        var emailValidar = EmailUsuarioValidacao.Validar(email);
        if (emailValidar.IsFailed) return Result.Fail(emailValidar.Errors);
        
        var senhaValidar = SenhaUsuarioValidacao.Validar(senha);
        if (senhaValidar.IsFailed) return Result.Fail(senhaValidar.Errors);

        Usuario usuario = Usuario.CriarUsuario(
            nome, email, senha, cpfValidar.Value
        );
        
        return Result.Ok(usuario);
    }
}