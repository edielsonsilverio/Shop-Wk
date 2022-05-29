using Shop.Core.Comunication;
using Shop.WebApp.MVC.ViewModels;

namespace Shop.WebApp.MVC.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> ObterTodos();
    Task<ProdutoViewModel> ObterPorId(Guid id);
    Task<ResponseResult> Adicionar(ProdutoViewModel model);
    Task<ResponseResult> Atualizar(ProdutoViewModel model);
    Task<ResponseResult> Excluir(ProdutoViewModel model);
}
