using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Core.DomainObjects;
using Shop.Core.WebApi.Controllers;
using Shop.Core.WebApi.Identidade;
using Shop.Core.WebApi.Usuario;
using Shop.WebApi.ViewModels;

namespace Shop.WebApi.Controllers;

//FOI COMENTADO PARA FUNCIONAR NO DOCKER
//[Authorize]
[Route("api/categoria")]
public class CategoriaController : MainController
{
    private readonly ICategoriaRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICategoriaService _repoService;

    public CategoriaController(
        ICategoriaRepository repo,
        IMapper mapper,
        ICategoriaService repoService,
        IAspNetUser user,
        INotificador notificacao) : base(notificacao, user)
    {
        _repo = repo;
        _mapper = mapper;
        _repoService = repoService;
    }
    //FOI COMENTADO PARA FUNCIONAR NO DOCKER
    //[ClaimsAuthorize("Categoria", "R")]
    [HttpGet("obtertodos")]
    public async Task<IActionResult> ObterTodos()
    {
        var lista = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _repo.ObterTodos()).ToList();
        return Ok(lista);
    }

    //[ClaimsAuthorize("Categoria", "R")]
    [HttpGet("consultar-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var objeto = _mapper.Map<CategoriaViewModel>(await ObterCategoria(id));
        return Ok(objeto);
    }

    //[ClaimsAuthorize("Categoria", "C")]
    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> Adicionar([FromBody] CategoriaViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Adicionar(_mapper.Map<Categoria>(viewModel)))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(viewModel);
    }

    //[ClaimsAuthorize("Categoria", "U")]
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<CategoriaViewModel>> Atualizar(Guid id, CategoriaViewModel viewModel)
    {
        if (id != viewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(viewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Atualizar(_mapper.Map<Categoria>(viewModel)))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(viewModel);
    }

    //[ClaimsAuthorize("Categoria", "D")]
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<CategoriaViewModel>> Excluir(Guid id)
    {
        var viewModel = await ObterCategoria(id);
        if (viewModel == null) return NotFound();

        var model = _mapper.Map<Categoria>(viewModel);
        await _repoService.Excluir(model);

        await PersistirDados();
        return CustomResponse(viewModel);
    }


    private async Task<CategoriaViewModel> ObterCategoria(Guid id)
    {
        return _mapper.Map<CategoriaViewModel>(await _repo.ObterPorId(id));
    }

    private async Task PersistirDados()
    {
        var result = await _repo.PersistirDados();
        if (!string.IsNullOrEmpty(result))
            AdicionarErroProcessamento(result);
    }
}
