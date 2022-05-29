using Microsoft.Extensions.Options;
using Shop.Core.Comunication;
using Shop.WebApp.MVC.Extensions;
using Shop.WebApp.MVC.ViewModels;

namespace Shop.WebApp.MVC.Services;

public class ProdutoService : Service, IProdutoService
{
    private readonly HttpClient _httpClient;

    public ProdutoService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.ApiUrl);

        _httpClient = httpClient;
    }

    public async Task<ProdutoViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/produto/consultar-id/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<ProdutoViewModel>(response);
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync($"/api/produto/obtertodos/");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
    }

    public async Task<ResponseResult> Adicionar(ProdutoViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PostAsync("/api/produto/", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> Atualizar(ProdutoViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/produto/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
    public async Task<ResponseResult> Excluir(ProdutoViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.DeleteAsync($"/api/produto/{model.Id}");

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
  
}