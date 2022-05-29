using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Core;
using Shop.Core.Messages.Integration;
using Shop.MessageBus;
using Shop.WebApp.MVC.Controllers;
using Shop.WebApp.MVC.Services;
using Shop.WebApp.MVC.ViewModels;

namespace NSE.WebApp.MVC.Controllers;

[Authorize]
public class ProdutoController : MainController
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;

    public ProdutoController(IProdutoService produtoService,
                             ICategoriaService categoriaService)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
    }

    [Route("produto")]
    public async Task<IActionResult> Index()
    {
        return View(await _produtoService.ObterTodos());
    }

    public async Task<IActionResult> Adicionar()
    {
        var model = new ProdutoViewModel
        {
            Categorias = (await ObterTodasCategorias()).ToList()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(ProdutoViewModel model)
    {

        if (!ModelState.IsValid) return View(model);

        var resposta = await _produtoService.Adicionar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Produto salvo com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Atualizar(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _produtoService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(ProdutoViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _produtoService.Atualizar(model);

        if (ResponsePossuiErros(resposta))
        {
            TempData["Erros"] =
            ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            return View(model);
        }

        TempData["success"] = "Produto salvo com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Excluir(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _produtoService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(ProdutoViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _produtoService.Excluir(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Produto excluido com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IEnumerable<SelectListItem>> ObterTodasCategorias()
    {
        return (await _categoriaService.ObterTodos()).Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });
    }

}