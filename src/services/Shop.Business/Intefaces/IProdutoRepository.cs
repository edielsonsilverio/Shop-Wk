using Shop.Business.Models;
using Shop.Core.Data;
using System.Linq.Expressions;

namespace Shop.Business.Intefaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterTodos(Expression<Func<Produto, bool>> filter = null);
    Task<Produto> ObterPorId(Guid modelId);
    Task<bool> Adicionar(Produto model);
    Task<bool> Atualizar(Produto model);
    Task<bool> Excluir(Produto model);

}
