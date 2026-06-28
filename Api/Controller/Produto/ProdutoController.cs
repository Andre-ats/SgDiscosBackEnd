using Api.Service.Arquivos;
using Aplicacao.Service.ArquivosStorage;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar.ProdutoAtualizarLinks.Enum;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;
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
    ProdutoAdicionarArquivosUseCase produtoAdicionarArquivosUseCase,
    ProdutoExcluirArquivosUseCase excluirArquivosUseCase
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
    [HttpPost("UploadArquivos")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadArquivos([FromForm] ArquivoStorageServiceInput arquivoLista, [FromHeader] Guid produtoId)
    {
        if (!arquivoLista.ArquivoLista.Any())
            return BadRequest("Nenhum arquivo foi enviado.");
        
        var arquivos = new List<ArquivoAdicionarInput>();

        foreach (var arquivo in arquivoLista.ArquivoLista)
        {
            if(arquivo.ContentType.StartsWith("image/"))
                arquivos.Add(new ArquivoAdicionarInput(arquivo, EnumTipoArquivo.Imagem));
            else if(arquivo.ContentType.StartsWith("video/"))
                arquivos.Add(new ArquivoAdicionarInput(arquivo, EnumTipoArquivo.Video));
            else
                return BadRequest($"Arquivo '{arquivo.FileName}' não é imagem nem vídeo.");
        }

        var response = await produtoAdicionarArquivosUseCase.Execute(
            new ProdutoAdicionarArquivosUseCaseInput(produtoId, arquivos));
        
        if (response.IsFailed)
            return BadRequest(response.Errors.Select(e => e.Message));

        return Ok(response.Value.Mensagem);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [HttpDelete("DeleteArquivo")]
    public async Task<IActionResult> DeleteArquivo(
        [FromBody] List<ArquivoExcluirInput> nomeArquivos,
        [FromHeader] Guid produtoId)
    {
        if (!nomeArquivos.Any())
            return BadRequest("Nenhum arquivo foi enviado.");

        foreach (var arquivo in nomeArquivos)
        {
            if (string.IsNullOrWhiteSpace(arquivo.PublicId))
                return BadRequest("PublicId inválido.");

            if (arquivo.EnumTipoArquivo != EnumTipoArquivo.Imagem &&
                arquivo.EnumTipoArquivo != EnumTipoArquivo.Video)
                return BadRequest($"Tipo inválido para o arquivo '{arquivo.PublicId}'.");
        }

        var response = await excluirArquivosUseCase.Execute(
            new ProdutoExcluirArquivosUseCaseInput(produtoId, nomeArquivos));

        if (response.IsFailed)
            return BadRequest(response.Errors.Select(e => e.Message));

        return Ok(response.Value.Mensagem);
    }
}