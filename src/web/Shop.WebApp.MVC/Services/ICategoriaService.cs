using Shop.Core.Comunication;
using Shop.WebApp.MVC.ViewModels;

namespace Shop.WebApp.MVC.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>> ObterTodos();
    Task<CategoriaViewModel> ObterPorId(Guid id);
    Task<ResponseResult> Adicionar(CategoriaViewModel model);
    Task<ResponseResult> Atualizar(CategoriaViewModel model);
    Task<ResponseResult> Excluir(CategoriaViewModel model);
}
