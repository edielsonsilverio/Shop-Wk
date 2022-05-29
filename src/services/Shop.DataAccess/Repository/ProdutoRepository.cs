using Microsoft.EntityFrameworkCore;
using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Core.Data;
using Shop.DataAccess.Context;
using System.Linq.Expressions;

namespace Shop.DataAccess.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ShopDbContext _context;

    public ProdutoRepository(ShopDbContext context) => _context = context;
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Produto>> ObterTodos(Expression<Func<Produto, bool>> filter = null)
    {
        //var query = _context.Produtos.Include(x => x.Categoria);
        var query = _context.Produtos;
        if (filter != null)
            return query.Where(filter);

        return query;
    }

    public async Task<Produto> ObterPorId(Guid modelId)
    {
        //return await _context.Produtos.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Id == modelId);
        return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == modelId);

    }
    public async Task<bool> Atualizar(Produto model)
    {
        _context.Produtos.Update(model);
        return Task.CompletedTask.IsCompleted;
    }
    public async Task<bool> Adicionar(Produto model)
    {
        await _context.Produtos.AddAsync(model);
        return Task.CompletedTask.IsCompleted;
    }
 
    public async Task<bool> Excluir(Produto model)
    {
        _context.Produtos.Remove(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<string> PersistirDados()
    {
        var result = await UnitOfWork.Commit();
        if (!result) return "Não foi possível persistir os dados no banco";

        return string.Empty;
    }
    public void Dispose() => _context.Dispose();
}