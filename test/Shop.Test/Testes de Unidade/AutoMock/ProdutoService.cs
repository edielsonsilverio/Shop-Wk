using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Core.Mediator;
using Shop.Core.Messages.Integration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Test;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMediatorHandler _mediator;

    public ProdutoService(IProdutoRepository ProdutoRepository, IMediatorHandler mediator)
    {
        _produtoRepository = ProdutoRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<Produto>> ObterTodos()
    {
        return await _produtoRepository.ObterTodos();
    }

    public async Task<bool> Adicionar(Produto model)
    {
        if (!model.EhValido())
            return false;

        await _produtoRepository.Adicionar(model);

        await _mediator.PublicarEvento(new EnviarEmailIntegrationEvent("admin@me.com", "admin@me.com", "Novo Registro", "Dê uma olhada!"));
       
        return true;
    }

    public async Task<bool> Atualizar(Produto model)
    {
        if (!model.EhValido())
            return false;

        await _produtoRepository.Atualizar(model);

        return true;

    }
    public void Dispose()
    {
        _produtoRepository.Dispose();
    }

    public async Task<bool> Excluir(Produto model)
    {
        await _produtoRepository.Excluir(model);

        return true;
    }
}