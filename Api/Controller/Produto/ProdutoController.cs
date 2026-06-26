using Api.Service.ArquivosStorage;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Produto;

[ApiController]
[Route("[controller]/[action]")]
public class ProdutoController(
    ProdutoCadastrarUseCase produtoCadastrarUseCase,
    ProdutoListarUseCase produtoListarUseCase,
    IArquivosStorageService arquivosStorageService
    ) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    [HttpPost(Name = "CadastrarProduto")]
    public ActionResult<ProdutoCadastrarUseCaseOutput> CadastrarProduto([FromBody] ProdutoCadastrarUseCaseInput input)
    {
        var result = produtoCadastrarUseCase.Execute(input);
        
        if (result.IsFailed)
            return BadRequest(new { erro = result.Errors.Select(e => e.Message) });

        return Created("", result.Value);
    }
    [AllowAnonymous]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    [HttpGet(Name = "ListarProdutos")]
    public ActionResult<ProdutoListarUseCaseOutput> ListarProdutos([FromQuery] ProdutoListarUseCaseInput input)
    {
        var result = produtoListarUseCase.Execute(input);
        
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpPost("UploadImagem")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImagem([FromForm] ArquivoStorageServiceInput arquivo)
    {
        var result = await arquivosStorageService.UploadImageAsync(arquivo.Arquivo);

        if (result.IsFailed)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok(result.Value.SecureUrl);
    }
    
    [HttpPost("UploadVideo")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadVideo([FromForm] ArquivoStorageServiceInput arquivo)
    {
        var result = await arquivosStorageService.UploadVideoAsync(arquivo.Arquivo);

        if (result.IsFailed)
            return BadRequest(result.Errors.Select(e => e.Message));

        return Ok(result.Value.SecureUrl);
    }
}