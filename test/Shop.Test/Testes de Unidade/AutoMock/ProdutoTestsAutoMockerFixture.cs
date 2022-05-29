using Bogus;
using Bogus.DataSets;
using Moq.AutoMock;
using Shop.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shop.Test;

[CollectionDefinition(nameof(ProdutoAutoMockerCollection))]
public class ProdutoAutoMockerCollection : ICollectionFixture<ProdutoTestsAutoMockerFixture>
{ }

public class ProdutoTestsAutoMockerFixture : IDisposable
{
    public ProdutoService ProdutoService;
    public AutoMocker Mocker;

    public Produto GerarClienteValido()
    {
        return GerarProdutos(1, true).FirstOrDefault();
    }

    public IEnumerable<Produto> ObterProdutos()
    {
        var clientes = new List<Produto>();

        clientes.AddRange(GerarProdutos(50, true).ToList());
        clientes.AddRange(GerarProdutos(50, false).ToList());

        return clientes;
    }

    public Produto GerarProdutoValido()
    {
        return GerarProdutos(1, true).FirstOrDefault();
    }

    public IEnumerable<Produto> GerarProdutos(int quantidade, bool ativo)
    {
        var genero = new Faker().PickRandom<Name.Gender>();

        var model = new Faker<Produto>("pt_BR")
            .CustomInstantiator(f => new Produto(
                 nome: f.Vehicle.Locale.FirstOrDefault().ToString(),
                descricao: f.Vehicle.Locale.FirstOrDefault().ToString(),
                valorCompra: 10,
                valorVenda: 50,
                estoqueMinimo: 10,
                estoqueMaximo: 1000));

        return model.Generate(quantidade);
    }

    public Produto GerarProdutoInvalido()
    {
        var genero = new Faker().PickRandom<Name.Gender>();

        var modelo = new Faker<Produto>("pt_BR")
             .CustomInstantiator(f => new Produto(
                nome: f.Vehicle.Locale.FirstOrDefault().ToString(),
                descricao: f.Vehicle.Locale.FirstOrDefault().ToString(),
                valorCompra: 10,
                valorVenda: 50,
                estoqueMinimo: 10,
                estoqueMaximo: 1000));

        return modelo;
    }

    public ProdutoService ObterProdutoService()
    {
        Mocker = new AutoMocker();
        ProdutoService = Mocker.CreateInstance<ProdutoService>();

        return ProdutoService;
    }

    public void Dispose()
    {
    }
}
