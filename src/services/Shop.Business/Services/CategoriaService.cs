using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Business.Validations;
using Shop.Core.DomainObjects;

namespace Shop.Business.Services;

public class CategoriaService : BaseService, ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository,
                         INotificador notificador) : base(notificador)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<bool> Adicionar(Categoria model)
    {
        if (!ExecutarValidacao(new CategoriaValidation(), model)) return false;

        if (_categoriaRepository.ObterTodos(f => f.Nome.ToLower() == model.Nome.ToLower()).Result.Any())
        {
            Notificar("Já existe uma categora com este nome.");
            return false;
        }

        await _categoriaRepository.Adicionar(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> Atualizar(Categoria model)
    {
        if (!ExecutarValidacao(new CategoriaValidation(), model)) return false;

        if (_categoriaRepository.ObterTodos(f => f.Nome != model.Nome && f.Id == model.Id).Result.Any())
        {
            Notificar("Não é possível alterar o nome.");
            return false;
        }

        await _categoriaRepository.Atualizar(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> Excluir(Categoria model)
    {
        await _categoriaRepository.Excluir(model);
        return Task.CompletedTask.IsCompleted;
    }

    public void Dispose() => _categoriaRepository?.Dispose();

   
}
