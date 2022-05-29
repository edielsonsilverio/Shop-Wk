using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Business.Validations;
using Shop.Core.DomainObjects;

namespace Shop.Business.Services;

public class ProdutoService : BaseService, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository,
                          INotificador notificador) : base(notificador)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> Adicionar(Produto model)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), model)) return false;

        if (_produtoRepository.ObterTodos(f => f.Nome == model.Nome).Result.Any())
        {
            Notificar("Já existe um produto com este nome.");
            return false;
        }
     
        await _produtoRepository.Adicionar(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> Atualizar(Produto model)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), model)) return false;

        if (_produtoRepository.ObterTodos(f => f.Nome != model.Nome && f.Id == model.Id).Result.Any())
        {
            Notificar("Não é possível alterar o nome.");
            return false;
        }
        await _produtoRepository.Atualizar(model);
        return Task.CompletedTask.IsCompleted;
    }

    public async Task<bool> Excluir(Produto model)
    {
        await _produtoRepository.Excluir(model);
        return Task.CompletedTask.IsCompleted;
    }

    public void Dispose() => _produtoRepository?.Dispose();
}
