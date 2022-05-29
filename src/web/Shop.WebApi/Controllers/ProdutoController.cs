using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Core.DomainObjects;
using Shop.Core.WebApi.Controllers;
using Shop.Core.WebApi.Identidade;
using Shop.Core.WebApi.Usuario;
using Shop.MessageBus;
using Shop.WebApi.Application.Commands;
using Shop.WebApi.ViewModels;

namespace Shop.WebApi.Controllers;

//[Authorize]
[Route("api/produto")]
public class ProdutoController : MainController
{
    private readonly IProdutoRepository _repo;
    private readonly IMapper _mapper;
    private readonly IProdutoService _repoService;
    private readonly IMediator _mediator;
    public ProdutoController(
        IProdutoRepository repo,
        IMapper mapper,
        IProdutoService repoService,
        IAspNetUser user,
        INotificador notificacao,
        IMediator mediator) : base(notificacao, user)
    {
        _repo = repo;
        _mapper = mapper;
        _repoService = repoService;
        _mediator = mediator;
    }

    [ClaimsAuthorize("Produto", "R")]
    [HttpGet]
    [Route("obtertodos")]
    public async Task<IActionResult> ObterTodos()
    {
        return Ok(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _repo.ObterTodos()));
    }

    [ClaimsAuthorize("Produto", "R")]
    [HttpGet("consultar-id/{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        return Ok(await ObterProduto(id));
    }


    [ClaimsAuthorize("Produto", "C")]
    [HttpPost]
    public async Task<ActionResult<ProdutoViewModel>> Adicionar([FromBody] ProdutoViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _repoService.Adicionar(_mapper.Map<Produto>(viewModel)))
            return CustomResponse(ModelState);

        NovoProdutoCommand data = ProdutoCommandExtension(viewModel);

        await _mediator.Send(data);

        //await PersistirDados();
        return CustomResponse(viewModel);
    }

    [ClaimsAuthorize("Produto", "U")]
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, ProdutoViewModel viewModel)
    {
        if (id != viewModel?.Id)
        {
            AdicionarErroProcessamento("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(viewModel);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var model = _mapper.Map<Produto>(viewModel);

        if (!await _repoService.Atualizar(model))
            return CustomResponse(ModelState);

        await PersistirDados();

        return CustomResponse(viewModel);
    }

    [ClaimsAuthorize("Produto", "D")]
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
    {
        var viewModel = await ObterProduto(id);
        if (viewModel == null) return NotFound();

        var model = _mapper.Map<Produto>(viewModel);
        await _repoService.Excluir(model);

        await PersistirDados();

        return CustomResponse(viewModel);
    }

    private async Task<ProdutoViewModel> ObterProduto(Guid id)
    {
        return _mapper.Map<ProdutoViewModel>(await _repo.ObterPorId(id));
    }


    private async Task PersistirDados()
    {
        var result = await _repo.PersistirDados();
        if (!string.IsNullOrEmpty(result))
            AdicionarErroProcessamento(result);
    }

    private async Task<ActionResult<ProdutoViewModel>> AdicionarProdutoRepositorio(ProdutoViewModel viewModel)
    {
        var model = _mapper.Map<Produto>(viewModel);
        if (!await _repoService.Adicionar(model))
        {
            AdicionarErroProcessamento("Erro ao tentar salvar o registro.");
            return CustomResponse(ModelState);
        }

        await PersistirDados();
        return CustomResponse(viewModel);
    }

    private static NovoProdutoCommand ProdutoCommandExtension(ProdutoViewModel viewModel)
    {
        return new NovoProdutoCommand
        {
            Nome = viewModel.Nome,
            Descricao = viewModel.Descricao,
            ValorCompra = viewModel.ValorCompra,
            ValorVenda = viewModel.ValorVenda,
            CategoriaId = viewModel.CategoriaId,
            EstoqueMinimo = viewModel.EstoqueMinimo,
            EstoqueMaximo = viewModel.EstoqueMaximo,
            QuantidadeEstoque = viewModel.QuantidadeEstoque,
        };
    }
}

