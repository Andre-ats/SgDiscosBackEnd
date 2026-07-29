namespace Api.Service.Arquivos;

public class ArquivoStorageServiceInput
{
    public List<IFormFile> ArquivoLista { get; set; } = [];
    public List<int> Ordens { get; set; } = [];
}