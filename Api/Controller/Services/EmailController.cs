using Aplicacao.UseCase.Email.EmailDuvidas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Services;

[ApiController]
[Route("[controller]/[action]")]
public class EmailController(
    EmailDuvidasUseCase emailDuvidasUseCase
    ) : ControllerBase
{
    [AllowAnonymous]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    [HttpPost(Name = "PostEmailDuvidas")]
    public async Task<IActionResult> PostEmailDuvidas([FromBody] EmailDuvidasUseCaseInput input)
    {
        var resultExecuteUseCase = await emailDuvidasUseCase.Execute(input);

        if (resultExecuteUseCase.IsFailed) 
            return BadRequest(resultExecuteUseCase.Errors.Select(e => e.Message));

        return Ok(resultExecuteUseCase.Value.Mensagem);
    }
}