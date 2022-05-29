using Shop.Business.Models;

namespace Shop.Business.Intefaces;

public interface ICategoriaService : IDisposable
{
    Task<bool> Adicionar(Categoria model);
    Task<bool> Atualizar(Categoria model);
    Task<bool> Excluir(Categoria model);
}
