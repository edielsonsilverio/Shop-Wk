using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.WebApp.MVC.Controllers;
using Shop.WebApp.MVC.Services;
using Shop.WebApp.MVC.ViewModels;

namespace NSE.WebApp.MVC.Controllers;

[Authorize]
public class CategoriaController : MainController
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [Route("categoria")]
    public async Task<IActionResult> Index()
    {
        return View(await _categoriaService.ObterTodos());
    }

    public async Task<IActionResult> Adicionar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(CategoriaViewModel model)
    {

        if (!ModelState.IsValid) return View(model);

        var resposta = await _categoriaService.Adicionar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Categoria salva com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Atualizar(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _categoriaService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Atualizar(CategoriaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _categoriaService.Atualizar(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Categoria salva com sucesso";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Excluir(Guid? id)
    {
        if (Guid.Empty == id)
            return NotFound();

        var model = await _categoriaService.ObterPorId(id.GetValueOrDefault());

        if (model == null)
            return NotFound();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(CategoriaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var resposta = await _categoriaService.Excluir(model);

        if (ResponsePossuiErros(resposta)) return View(model);

        TempData["success"] = "Categoria excluida com sucesso";
        return RedirectToAction("Index");
    }
}