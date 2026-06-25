using Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Produto;

[ApiController]
[Route("[controller]/[action]")]
public class ProdutoController(
    ProdutoCadastrarUseCase produtoCadastrarUseCase
    ) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.HttpPost(Name = "CadastrarProduto")]
    public ActionResult<ProdutoCadastrarUseCaseOutput> CadastrarProduto([FromBody] ProdutoCadastrarUseCaseInput input)
    {
        var result = produtoCadastrarUseCase.Execute(input);
        
        if (result.IsFailed)
            return BadRequest(new { erro = result.Errors.Select(e => e.Message) });

        return Created("", result.Value);
    }
}