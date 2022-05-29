using Shop.Business.Models;

namespace Shop.Business.Intefaces;

public interface IProdutoService : IDisposable
{
    Task<bool> Adicionar(Produto model);
    Task<bool> Atualizar(Produto model);
    Task<bool> Excluir(Produto model);
}