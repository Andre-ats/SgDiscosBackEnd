using Aplicacao.Service.ArquivosStorage;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarLinks;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
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
    IArquivosStorageService arquivosStorageService,
    ProdutoAdicionarLinksUseCase produtoAdicionarLinksUseCase
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
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [HttpPost("UploadImagem")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImagem([FromForm] ArquivoStorageServiceInput arquivoLista, [FromHeader] Guid produtoId)
    {
        if (!arquivoLista.ArquivoLista.Any())
            return BadRequest("Nenhuma imagem foi enviada.");

        var response = await produtoAdicionarLinksUseCase.Execute(
            new ProdutoAdicionarLinksUseCaseInput(produtoId, EnumTipoArquivo.Imagem, arquivoLista.ArquivoLista));
        
        if (response.IsFailed)
            return BadRequest(response.Errors.Select(e => e.Message));

        return Ok(response.Value.Mensagem);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [HttpPost("UploadVideo")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadVideo([FromForm] ArquivoStorageServiceInput arquivoLista, [FromHeader] Guid produtoId)
    {
        if (!arquivoLista.ArquivoLista.Any())
            return BadRequest("Nenhum vídeo foi enviado.");

        var response = await produtoAdicionarLinksUseCase.Execute(
            new ProdutoAdicionarLinksUseCaseInput(produtoId, EnumTipoArquivo.Video, arquivoLista.ArquivoLista));
        
        if (response.IsFailed)
            return BadRequest(response.Errors.Select(e => e.Message));

        return Ok(response.Value.Mensagem);
    }
}