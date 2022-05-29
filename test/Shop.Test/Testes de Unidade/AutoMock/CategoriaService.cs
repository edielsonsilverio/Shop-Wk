using MediatR;
using Shop.Business.Intefaces;
using Shop.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Test;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository modelRepository)
    {
        _categoriaRepository = modelRepository;
    }

    public async Task<IEnumerable<Categoria>> ObterTodos()
    {
        return await _categoriaRepository.ObterTodos();
    }

    public async Task<bool> Adicionar(Categoria model)
    {
        if (!model.EhValido())
            return false;

        return await _categoriaRepository.Adicionar(model);

    }

    public async Task<bool> Atualizar(Categoria model)
    {
        if (!model.EhValido())
            return false;

        return await _categoriaRepository.Atualizar(model);

    }

    public async Task<bool> Excluir(Categoria model)
    {
        return await _categoriaRepository.Excluir(model);
    }

    public void Dispose()
    {
        _categoriaRepository.Dispose();
    }

 
}
