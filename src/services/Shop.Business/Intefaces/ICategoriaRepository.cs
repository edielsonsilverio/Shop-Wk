using Shop.Business.Models;
using Shop.Core.Data;
using System.Linq.Expressions;

namespace Shop.Business.Intefaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IEnumerable<Categoria>> ObterTodos(Expression<Func<Categoria, bool>> filter = null);
    Task<Categoria> ObterPorId(Guid modelId);
    Task<bool> Adicionar(Categoria model);
    Task<bool> Atualizar(Categoria model);
    Task<bool> Excluir(Categoria model);
}
