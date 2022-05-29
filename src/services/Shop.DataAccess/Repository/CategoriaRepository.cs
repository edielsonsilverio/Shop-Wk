using Microsoft.EntityFrameworkCore;
using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Core.Data;
using Shop.DataAccess.Context;
using System.Linq.Expressions;

namespace Shop.DataAccess.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ShopDbContext _context;

    public CategoriaRepository(ShopDbContext context) => _context = context;
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Categoria>> ObterTodos(Expression<Func<Categoria, bool>> filter = null)
    {
        var query = _context.Categorias;

        if (filter != null)
           return  await query.Where(filter).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<Categoria> ObterPorId(Guid modelId)
    {
        return await _context.Categorias.FindAsync(modelId);
    }

    public async Task<bool> Adicionar(Categoria model)
    {
        await _context.Categorias.AddAsync(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> Atualizar(Categoria model)
    {
        _context.Categorias.Update(model);
        return Task.CompletedTask.IsCompleted;
    }
    public async Task<string> PersistirDados()
    {
        await UnitOfWork.Commit();

        var result = await UnitOfWork.Commit();
        if (!result) return "Não foi possível persistir os dados no banco";

        return string.Empty;
    }

    public async Task<bool> Excluir(Categoria model)
    {
        _context.Categorias.Remove(model);
        return Task.CompletedTask.IsCompleted;
    }

    public void Dispose() => _context.Dispose();

   
}
