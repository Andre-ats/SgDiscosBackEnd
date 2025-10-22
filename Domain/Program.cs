using Domain.Entidade.UsuarioEntidade;

Usuario usuario = Usuario.CriarUsuario("Teste", "teste@gmail.com", "OIOIawkdakdwad", "11111111111");


Console.WriteLine(usuario.Senha);