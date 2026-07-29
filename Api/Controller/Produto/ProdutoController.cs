using Api.Service.Arquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAdicionarArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoAtualizar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoCadastrar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoExcluirArquivos;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListar;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoListagem.ProdutoListarById;
using Aplicacao.UseCase.ProdutoUseCase.ProdutoMudarStatus;
using Domain.Entidade.ProdutoEntidade.EnumsProdutoEntidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Produto;

[ApiController]
[Route("[controller]/[action]")]
public class ProdutoController(
    ProdutoCadastrarUseCase produtoCadastrarUseCase,
    ProdutoListarUseCase produtoListarUseCase,
    ProdutoAdicionarArquivosUseCase produtoAdicionarArquivosUseCase,
    ProdutoExcluirArquivosUseCase excluirArquivosUseCase,
    ProdutoMudarStatusUseCase produtoMudarStatusUseCase,
    ProdutoListarByIdUseCase produtoListarByIdUseCase,
    ProdutoAtualizarUseCase produtoAtualizarUseCase
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
    
    [AllowAnonymous]
    [ProducesResponseType(201)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    [HttpGet(Name = "ListarProdutosById")]
    public ActionResult<ProdutoListarUseCaseOutput> ListarProdutosById([FromQuery] ProdutoListarByIdUseCaseInput input)
    {
        var result = produtoListarByIdUseCase.Execute(input);
        
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
        if (arquivoLista.ArquivoLista.Count == 0)
            return BadRequest("Nenhum arquivo foi enviado.");

        if (arquivoLista.ArquivoLista.Count != arquivoLista.Ordens.Count)
            return BadRequest("A quantidade de arquivos e ordens é diferente.");

        var arquivos = new List<ArquivoAdicionarInput>();

        for (var i = 0; i < arquivoLista.ArquivoLista.Count; i++)
        {
            var arquivo = arquivoLista.ArquivoLista[i];
            var ordem = arquivoLista.Ordens[i];

            if (arquivo.ContentType.StartsWith("image/"))
            {
                arquivos.Add(new ArquivoAdicionarInput(
                    arquivo,
                    EnumTipoArquivoProduto.Imagem,
                    ordem
                ));
            }
            else if (arquivo.ContentType.StartsWith("video/"))
            {
                arquivos.Add(new ArquivoAdicionarInput(
                    arquivo,
                    EnumTipoArquivoProduto.Video,
                    ordem
                ));
            }
            else
            {
                return BadRequest(
                    $"Arquivo '{arquivo.FileName}' não é imagem nem vídeo."
                );
            }
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

            if (arquivo.EnumTipoArquivo != EnumTipoArquivoProduto.Imagem &&
                arquivo.EnumTipoArquivo != EnumTipoArquivoProduto.Video)
                return BadRequest($"Tipo inválido para o arquivo '{arquivo.PublicId}'.");
        }

        var response = await excluirArquivosUseCase.Execute(
            new ProdutoExcluirArquivosUseCaseInput(produtoId, nomeArquivos));

        if (response.IsFailed)
            return BadRequest(response.Errors.Select(e => e.Message));

        return Ok(response.Value.Mensagem);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [HttpPut("UpdateStatusProduto")]
    public IActionResult UpdateStatusProduto([FromBody] ProdutoMudarStatusUseCaseInput produtoMudarStatusUseCaseInput) {
        
        var resultExecuteUseCase = produtoMudarStatusUseCase.Execute(produtoMudarStatusUseCaseInput);

        if (resultExecuteUseCase.IsFailed) 
            return BadRequest(resultExecuteUseCase.Errors.Select(e => e.Message));

        return Ok(resultExecuteUseCase.Value.Mensagem);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [HttpPut("UpdateProduto")]
    public IActionResult UpdateProduto([FromBody] ProdutoAtualizarUseCaseInput produtoMudarStatusUseCaseInput) {
        
        var resultExecuteUseCase = produtoAtualizarUseCase.Execute(produtoMudarStatusUseCaseInput);

        if (resultExecuteUseCase.IsFailed) 
            return BadRequest(resultExecuteUseCase.Errors.Select(e => e.Message));

        return Ok(resultExecuteUseCase.Value);
    }
}