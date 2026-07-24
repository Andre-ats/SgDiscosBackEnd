using Api.DTO;
using Api.Service.TokenService;
using Aplicacao.UseCase.AdminUseCase.AdminLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Admin;

[ApiController]
[Route("[controller]/[action]")]
public class AdminController(
    AdminLoginUseCase adminLoginUseCase,
    ITokenGenerator tokenGenerator
) : ControllerBase
{
    [AllowAnonymous]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    [HttpPost(Name = "LoginAdmin")]
    public ActionResult<TokenOutput> LoginAdmin([FromBody] AdminLoginUseCaseInput input)
    {
        var usuario = adminLoginUseCase.Execute(input);

        if (usuario.IsFailed)
        {
            var erro = usuario.Errors.First().Message;

            if (erro.Contains("inválido", StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new { error = erro });

            return BadRequest(new { error = erro });
        }

        var admin = usuario.Value.Admin;
        UsuarioToken usuarioToken = new UsuarioToken(admin.Id, admin.Nome, admin.Email!, "Admin");
        var token = tokenGenerator.CreateToken(usuarioToken);

        return Created("", new TokenOutput
        {
            Token = token
        });
    }
}