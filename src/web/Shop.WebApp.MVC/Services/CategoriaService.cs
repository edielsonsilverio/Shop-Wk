using Microsoft.Extensions.Options;
using Shop.Core.Comunication;
using Shop.WebApp.MVC.Extensions;
using Shop.WebApp.MVC.ViewModels;

namespace Shop.WebApp.MVC.Services;

public class CategoriaService : Service, ICategoriaService
{
    private readonly HttpClient _httpClient;

    public CategoriaService(HttpClient httpClient,
        IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.ApiUrl);

        _httpClient = httpClient;
    }

    public async Task<CategoriaViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/categoria/consultar-id/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<CategoriaViewModel>(response);
    }

    public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
    {
        var response = await _httpClient.GetAsync($"/api/categoria/obtertodos/");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<IEnumerable<CategoriaViewModel>>(response);
    }

    public async Task<ResponseResult> Adicionar(CategoriaViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PostAsync("/api/categoria/", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> Atualizar(CategoriaViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.PutAsync($"/api/categoria/{model.Id}", modelContent);

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }

    public async Task<ResponseResult> Excluir(CategoriaViewModel model)
    {
        var modelContent = ObterConteudo(model);

        var response = await _httpClient.DeleteAsync($"/api/categoria/{model.Id}");

        if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        return RetornoOk();
    }
}